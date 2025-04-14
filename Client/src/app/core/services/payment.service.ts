import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Paging } from '../../shared/models/paging';
import { NewPayment, Payment } from '../../shared/models/payment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  baseUrl = environment.apiUrl;
  controllerName = "payments";
  private http = inject(HttpClient);

  getPayments(paginationParams: PaginationParams) {
    let params = new HttpParams();

    //if (paginationParams.filter) {
    //  params = params.append("filter", `Patient.FirstName=*${paginationParams.filter}|Patient.LastName=*${paginationParams.filter}`);
    //}
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<Payment>>(`${this.baseUrl}${this.controllerName}`, { params })
  }

  getPaymentById(id: number) {
    return this.http.get<Payment>(`${this.baseUrl}${this.controllerName}/${id}`)
  }

  createPayment(payment: NewPayment) {
    return this.http.post<number>(`${this.baseUrl}${this.controllerName}`, payment);
  }
}
