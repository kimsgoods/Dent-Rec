import { Component, inject } from '@angular/core';
import { CustomTableComponent } from '../../shared/components/custom-table/custom-table.component';
import { PaymentService } from '../../core/services/payment.service';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Payment } from '../../shared/models/payment';

@Component({
  selector: 'app-payments',
  imports: [
    CustomTableComponent
  ],
  templateUrl: './payments.component.html',
  styleUrl: './payments.component.scss'
})
export class PaymentsComponent {
  private paymentService = inject(PaymentService);
  private router = inject(Router);
  paginationParams = new PaginationParams();
  totalItems = 0;
  payments: Payment[] = [];
  title = "Payment Records"
  defaultSortField = "paymentDate"
  defaultSortDirection: "asc" | "desc" = "desc"

  getPayments() {
    this.paymentService.getPayments(this.paginationParams).subscribe({
      next: response => {
        this.payments = response.data,
          this.totalItems = response.count
      },
      error: error => console.log(error)
    });
  }

  ngOnInit(): void {
    this.updateFilter(this.defaultSortField, this.defaultSortDirection);
    this.getPayments();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getPayments();
  }

  updateFilter(field: string, direction: string) {
    let orderByField = "";
    switch (field) {
      case "paymentDate":
        orderByField = "createdOn"
        break;
      case "patientName":
        orderByField = "Patient.FirstName"
        break;
      default:
        orderByField = field
        break;
    }
    this.paginationParams.orderBy = `${orderByField} ${direction}`;
  }
  onSortChange(event: { field: string, direction: 'asc' | 'desc' }) {
    this.updateFilter(event.field, event.direction)
    this.getPayments();
  }

  columns = [
    { field: 'patientName', header: 'Patient Name' },
    { field: 'amount', header: 'Amount', pipe: 'currency', pipeArgs: 'PHP' },
    { field: 'paymentMethod', header: 'Payment Method' },
    { field: 'paymentDate', header: 'Payment Date', pipe: 'date', pipeArgs: 'short' }
  ]

  actions = [
    {
      label: 'View',
      icon: 'visibility',
      tooltip: 'View payment details',
      action: (row: any) => {
        this.router.navigateByUrl(`/patient-logs/${row.patientLogId}`)
      }
    }
  ];

  onAction(action: (row: any) => void, row: any) {
    action(row);
  }
}
