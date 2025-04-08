import { Component, inject, OnInit } from '@angular/core';
import { CustomTableComponent } from '../../shared/components/custom-table/custom-table.component';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Router } from '@angular/router';
import { PatientService } from '../../core/services/patient.service';
import { MatDialog } from '@angular/material/dialog';
import { Patient } from '../../shared/models/patient';
import { PageEvent } from '@angular/material/paginator';
import { PatientFormComponent as PatientFormComponent } from '../patient-form/patient-form.component';
import { firstValueFrom } from 'rxjs';
import { DialogService } from '../../core/services/dialog.service';

@Component({
  selector: 'app-patients',
  imports: [
    CustomTableComponent
  ],
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.scss'
})
export class PatientsComponent {
  private patientService = inject(PatientService);
  private dialogService = inject(DialogService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  paginationParams = new PaginationParams();
  totalItems = 0;
  patients: Patient[] = [];
  title = "Patient Records"

  getPatients() {
    this.patientService.getPatients(this.paginationParams).subscribe({
      next: response => {
        this.patients = response.data,
          this.totalItems = response.count
      },
      error: error => console.log(error)
    });
  }

  ngOnInit(): void {
    this.paginationParams.orderBy = "firstName asc"
    this.getPatients();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getPatients();
  }

  onSortChange(event: { field: string, direction: 'asc' | 'desc' }) {
    this.paginationParams.orderBy = `${event.field} ${event.direction}`;
    this.getPatients();
  }

  columns = [
    { field: 'firstName', header: 'First Name' },
    { field: 'lastName', header: 'Last Name' },
    { field: 'age', header: 'Age' },
    { field: 'gender', header: 'Sex' },
    { field: 'address', header: 'Address' },
    { field: 'phone', header: 'Phone' },
    { field: 'email', header: 'Email' },
  ]

  actions = [
    {
      label: 'View',
      icon: 'visibility',
      tooltip: 'View patient details',
      action: (row: any) => {
        this.router.navigateByUrl(`/patients/${row.id}`)
      }
    },
    {
      label: 'Edit',
      icon: 'edit',
      tooltip: 'Edit patient',
      action: (row: any) => {
        this.openEditDialog(row)
      }
    },
    {
      label: 'Delete',
      icon: 'delete',
      tooltip: 'Delete patient',
      action: (row: any) => {
        this.openConfirmDialog(row.id)
      }
    }
  ];

  onAction(action: (row: any) => void, row: any) {
    action(row);
  }

  openCreateDialog() {
    const dialog = this.dialog.open(PatientFormComponent, {
      minWidth: '500px',
      data: {
        title: 'Create patient'
      }
    });
    dialog.afterClosed().subscribe({
      next: async result => {
        if (result) {
          const patient = await firstValueFrom(this.patientService.createPatient(result.patient));
          if (patient) {
            this.patients.push(patient);
            this.getPatients();
          }
        }
      }
    })
  }

  openEditDialog(patient: Patient) {
    const dialog = this.dialog.open(PatientFormComponent, {
      minWidth: '500px',
      data: {
        title: 'Edit patient',
        patient
      }
    })
    dialog.afterClosed().subscribe({
      next: async result => {
        if (result) {
          await firstValueFrom(this.patientService.updatePatient(result.patient));
          const index = this.patients.findIndex(p => p.id === result.patient.id);
          if (index !== -1) {
            this.patients[index] = result.patient;
          }
          this.getPatients();
        }
      }
    })
  }

  async openConfirmDialog(id: number) {
    const confirmed = await this.dialogService.confirm(
      'Confirm delete patient',
      'Are you sure you want to delete this patient? This cannot be undone'
    );
    if (confirmed) this.onDelete(id);
  }

  onDelete(id: number) {
    this.patientService.deletePatient(id).subscribe({
      next: () => {
        this.patients = this.patients.filter(x => x.id !== id);
        this.getPatients();
      }
    })
  }

}
