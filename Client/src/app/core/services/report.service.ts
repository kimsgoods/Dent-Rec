import { inject, Injectable } from '@angular/core';
import { DailyReport } from '../../shared/models/dailyReport';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Paging } from '../../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  baseUrl = environment.apiUrl;
  controllerName = "reports";
  private http = inject(HttpClient);

  

  getDailyReports(paginationParams: PaginationParams) {
    let params = new HttpParams();

    if (paginationParams.filter) {
      params = params.append("filter", paginationParams.filter);
    }
    if (paginationParams.orderBy) {
      params = params.append("orderBy", paginationParams.orderBy);
    }

    params = params.append("pageSize", paginationParams.pageSize);
    params = params.append("page", paginationParams.page);

    return this.http.get<Paging<DailyReport>>(`${this.baseUrl}${this.controllerName}`, { params })
  }
}
