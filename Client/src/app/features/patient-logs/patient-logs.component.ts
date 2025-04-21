import { Component, inject, OnInit } from '@angular/core';
import { PatientLogService } from '../../core/services/patient-log.service';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Paging } from '../../shared/models/paging';
import { PatientLog } from '../../shared/models/patientLog';
import { Router } from '@angular/router';
import { CustomTableComponent } from '../../shared/components/custom-table/custom-table.component';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-patient-logs',
  imports: [
    CustomTableComponent
  ],
  templateUrl: './patient-logs.component.html',
  styleUrl: './patient-logs.component.scss'
})
export class PatientLogsComponent implements OnInit {
  private patientProcedureService = inject(PatientLogService);
  private router = inject(Router);
  paginationParams = new PaginationParams();
  patientLogs?: Paging<PatientLog>
  totalItems = 0;
  logs: PatientLog[] = [];
  title = "Patient Logs"
  defaultSortField = "procedureDate"
  defaultSortDirection: "asc" | "desc" = "desc"
 
  columns = [
    { field: 'patientName', header: 'Name' },
    { field: 'age', header: 'Age' },
    { field: 'gender', header: 'Sex' },
    { field: 'address', header: 'Address' },
    { field: 'procedures', header: 'Procedure', sortable: false },
    { field: 'procedureDate', header: 'Date', pipe: 'date', pipeArgs: 'short' },
    { field: 'fee', header: 'Fee', pipe: 'currency', pipeArgs: 'PHP' },
    { field: 'paymentStatus', header: 'Payment Status' },
    { field: 'notes', header: 'Notes' }
  ];

  actions = [
    {
      label: 'View',
      icon: 'visibility',
      tooltip: 'View patient log details',
      action: (row: any) => {
        this.router.navigateByUrl(`/patient-logs/${row.id}`)
      }
    }
  ]

  getPatientLogs() {
    this.patientProcedureService.getPatientLogs(this.paginationParams).subscribe({
      next: response => {
        this.patientLogs = response,
          this.logs = response.data,
          this.totalItems = response.count
      },
      error: error => console.log(error)
    });
  }

  ngOnInit(): void {
    this.updateFilter(this.defaultSortField, this.defaultSortDirection);
    this.getPatientLogs();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getPatientLogs();
  }

  onFilterChange(newFilter: string) {
    this.paginationParams.filter = newFilter;
    this.paginationParams.page = 1;
    this.getPatientLogs();
  }

  openCreateNewForm() {
    this.router.navigateByUrl("/patient-logs-form");
  }  

  onAction(action: (row: any) => void, row: any) {
    action(row);
  }

  onSortChange(event: { field: string, direction: 'asc' | 'desc' }) {
    this.updateFilter(event.field, event.direction);
    this.getPatientLogs();
  }

  updateFilter(field: string, direction: string) {
    let orderByField = "";
    switch (field) {
      case "age":
        orderByField = "patientAge"
        break;
      case "patientName":
        orderByField = "Patient.FirstName"
        break;
      case "address":
        orderByField = "Patient.Address"
        break;
      case "gender":
        orderByField = "Patient.Gender"
        break;
      default:
        orderByField = field
        break;
    }
    this.paginationParams.orderBy = `${orderByField} ${direction}`;
  }
  
}
