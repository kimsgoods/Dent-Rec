import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Procedure } from '../../shared/models/procedure';
import { Paging } from '../../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class ProcedureService {

  baseUrl = environment.apiUrl;
  controllerName = "procedures";
  private http = inject(HttpClient);

  createProcedure(procedure: Procedure) {
    return this.http.post<number>(`${this.baseUrl}${this.controllerName}`, procedure);
  }

  updateProcedure(procedure: Procedure) {
    return this.http.put(`${this.baseUrl}${this.controllerName}`, procedure);
  }

  getProcedures(paginationParams: PaginationParams) {
    let params = new HttpParams();

    if (paginationParams.filter) {
      params = params.append("filter", paginationParams.filter);
    }
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<Procedure>>(`${this.baseUrl}${this.controllerName}`, { params })
  }

  getProcedureById(id: number) {
    return this.http.get<Procedure>(`${this.baseUrl}${this.controllerName}/${id}`)
  }

  deleteProcedure(id: number) {
    return this.http.delete(`${this.baseUrl}${this.controllerName}/${id}`)
  }


}
