import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Patient } from '../../shared/models/patient';
import { Paging } from '../../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  createPatient(patient: Patient) {
    return this.http.post<Patient>(`${this.baseUrl}patients`, patient);
  }

  updatePatient(patient: Patient) {
    return this.http.put(`${this.baseUrl}patients`, patient);
  }

  getPatients(paginationParams: PaginationParams) {
    let params = new HttpParams();

    if (paginationParams.filter) {
      params = params.append("filter", `FirstName=*${paginationParams.filter}|LastName=*${paginationParams.filter}`);
    }
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<Patient>>(`${this.baseUrl}patients`, { params })
  }

  getPatientLogById(id: number) {
    return this.http.get<Patient>(`${this.baseUrl}patients/${id}`)
  }

  deletePatient(id: number) {
    return this.http.delete(`${this.baseUrl}patients/${id}`)
  }


}
