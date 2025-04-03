import { Component, inject, OnInit } from '@angular/core';
import { PatientLogService } from '../../core/services/patient-procedure.service';
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
    this.paginationParams.orderBy = "procedureDate desc"
    this.getPatientLogs();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getPatientLogs();
  }

  columns = [
    { field: 'patientName', header: 'Name' },
    { field: 'age', header: 'Age' },
    { field: 'gender', header: 'Sex' },
    { field: 'address', header: 'Address' },
    { field: 'procedures', header: 'Procedure' },
    { field: 'procedureDate', header: 'Date', pipe: 'date', pipeArgs: 'short' },
    { field: 'fee', header: 'Fee', pipe: 'currency', pipeArgs: 'PHP' },
    { field: 'paymentStatus', header: 'Payment Status' }
  ];

  actions = [
    {
      label: 'View',
      icon: 'visibility',
      tooltip: 'View Order',
      action: (row: any) => {
        this.router.navigateByUrl(`/patient-logs/${row.id}`)
      }
    }
  ]

  onAction(action: (row: any) => void, row: any) {
    action(row);
  }
}
