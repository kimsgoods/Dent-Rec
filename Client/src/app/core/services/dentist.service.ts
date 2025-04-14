import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Dentist } from '../../shared/models/dentist';
import { Paging } from '../../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class DentistService {

  baseUrl = environment.apiUrl;
  controllerName = "dentists";
  private http = inject(HttpClient);

  createDentist(dentist: Dentist) {
    return this.http.post<number>(`${this.baseUrl}${this.controllerName}`, dentist);
  }

  updateDentist(dentist: Dentist) {
    return this.http.put(`${this.baseUrl}${this.controllerName}`, dentist);
  }

  getDentists(paginationParams: PaginationParams) {
    let params = new HttpParams();

    if (paginationParams.filter) {
      params = params.append("filter", paginationParams.filter);
    }
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<Dentist>>(`${this.baseUrl}${this.controllerName}`, { params })
  }

  getDentistById(id: number) {
    return this.http.get<Dentist>(`${this.baseUrl}${this.controllerName}/${id}`)
  }

  deleteDentist(id: number) {
    return this.http.delete(`${this.baseUrl}${this.controllerName}/${id}`)
  }


}
