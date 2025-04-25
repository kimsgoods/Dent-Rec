import { Component, inject } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { PaginationParams } from '../../shared/models/paginationParams';
import { CustomTableComponent } from '../../shared/components/custom-table/custom-table.component';
import { ReportService } from '../../core/services/report.service';
import { DailyReport } from '../../shared/models/dailyReport';

@Component({
  selector: 'app-reports',
  imports: [
    CustomTableComponent
  ],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.scss'
})
export class ReportsComponent {
  private reportService = inject(ReportService);
  paginationParams = new PaginationParams();
  totalItems = 0;
  reports: DailyReport[] = [];
  title = "Daily Reports"
  defaultSortField = "date"
  defaultSortDirection: "asc" | "desc" = "desc"

  getDailyReport() {
    this.reportService.getDailyReports(this.paginationParams).subscribe({
      next: response => {
        this.reports = response.data        
        this.totalItems = response.count
      },
      error: error => console.log(error)
    });
  }

  ngOnInit(): void {
    this.updateFilter(this.defaultSortField, this.defaultSortDirection);
    this.getDailyReport();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getDailyReport();
  }

  updateFilter(field: string, direction: string) {
    let orderByField = "";
    switch (field) {
        default:
        orderByField = field
        break;
    }
    this.paginationParams.orderBy = `${orderByField} ${direction}`;
  }
  onSortChange(event: { field: string, direction: 'asc' | 'desc' }) {
    this.updateFilter(event.field, event.direction)
    this.getDailyReport();
  }

  columns = [
    { field: 'date', header: 'Date', pipe: 'date', pipeArgs: { dateStyle: 'long' } },
    { field: 'date', header: 'Day', pipe: 'date', pipeArgs: { weekday: 'long' }, sortable: false },
    { field: 'morningPatientCount', header: 'Morning Patients' },
    { field: 'afternoonPatientCount', header: 'Afternoon Patients' },
    { field: 'dailyPatientCount', header: 'Total Patients' },    
    { field: 'cashPayment', header: 'Cash Payment', pipe: 'currency', pipeArgs: 'PHP' },    
    { field: 'gCashPayment', header: 'GCash Payment', pipe: 'currency', pipeArgs: 'PHP' },
    { field: 'totalPaymentAmount', header: 'Total Amount', pipe: 'currency', pipeArgs: 'PHP' },
  ]
}
