import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Dentist } from '../../shared/models/dentist';
import { Paging } from '../../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class dentistService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  createDentist(dentist: Dentist) {
    return this.http.post<Dentist>(`${this.baseUrl}dentists`, dentist);
  }

  updateDentist(dentist: Dentist) {
    return this.http.put(`${this.baseUrl}dentists`, dentist);
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

    return this.http.get<Paging<Dentist>>(`${this.baseUrl}dentists`, { params })
  }

  getDentistById(id: number) {
    return this.http.get<Dentist>(`${this.baseUrl}dentists/${id}`)
  }

  deleteDentist(id: number) {
    return this.http.delete(`${this.baseUrl}dentists/${id}`)
  }


}
