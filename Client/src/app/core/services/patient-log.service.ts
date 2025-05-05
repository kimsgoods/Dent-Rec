import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { PatientLog, UpdatePatientLogNotes } from '../../shared/models/patientLog';
import { Paging } from '../../shared/models/paging';
import { PatientLogDetails } from '../../shared/models/patientLogDetails';

@Injectable({
  providedIn: 'root'
})
export class PatientLogService {

  baseUrl = environment.apiUrl;
  controllerName = "patientlogs";
  private http = inject(HttpClient);

  getPatientLogs(paginationParams: PaginationParams) {
    let params = new HttpParams();

    if (paginationParams.filter) {
      params = params.append("filter", paginationParams.filter);
    }
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<PatientLog>>(`${this.baseUrl}${this.controllerName}`, { params })
  }

  getPatientLogById(id: number) {
    return this.http.get<PatientLogDetails>(`${this.baseUrl}${this.controllerName}/${id}`)
  }

  createPatientLog(log: any) {
    return this.http.post<number>(`${this.baseUrl}${this.controllerName}`, log);
  }

  updatePatientLog(log: UpdatePatientLogNotes) {
    return this.http.put(`${this.baseUrl}${this.controllerName}`, log);
  }

}
