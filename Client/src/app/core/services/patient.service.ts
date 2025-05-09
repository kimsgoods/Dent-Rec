import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Patient, PatientDetails } from '../../shared/models/patient';
import { Paging } from '../../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  baseUrl = environment.apiUrl;
  controllerName = "patients";
  private http = inject(HttpClient);

  createPatient(patient: Patient) {
    return this.http.post<number>(`${this.baseUrl}${this.controllerName}`, patient);
  }

  updatePatient(patient: Patient) {
    return this.http.put(`${this.baseUrl}${this.controllerName}`, patient);
  }

  getPatients(paginationParams: PaginationParams) {
    let params = new HttpParams();

    if (paginationParams.filter) {
      params = params.append("filter", paginationParams.filter);
    }
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<Patient>>(`${this.baseUrl}${this.controllerName}`, { params })
  }

  getPatientById(id: number) {
    return this.http.get<PatientDetails>(`${this.baseUrl}patients/${id}`)
  }

  deletePatient(id: number) {
    return this.http.delete(`${this.baseUrl}patients/${id}`)
  }


}
