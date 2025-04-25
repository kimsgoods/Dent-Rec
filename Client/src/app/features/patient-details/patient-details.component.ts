import { Component, inject, OnInit } from '@angular/core';
import { PatientService } from '../../core/services/patient.service';
import { Patient, PatientDetails } from '../../shared/models/patient';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule, DatePipe, Location } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { PatientFormComponent } from '../patient-form/patient-form.component';
import { MatDialog } from '@angular/material/dialog';
import { firstValueFrom } from 'rxjs';
import { SnackbarService } from '../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-patient-details',
  imports: [
    DatePipe,
    CommonModule,
    MatIcon,
    MatButton
  ],
  templateUrl: './patient-details.component.html',
  styleUrl: './patient-details.component.scss'
})
export class PatientDetailsComponent implements OnInit {
  private patientService = inject(PatientService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private snackbarService = inject(SnackbarService);  
  private location = inject(Location);
  patient!: PatientDetails;
  activeTab: "logs" | "payments" = "logs";


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.getPatientDetails(+id);
    }
  }

  getPatientDetails(id: number): void {
    this.patientService.getPatientById(id).subscribe({
      next: (data) => {
        this.patient = data;
      },
      error: (err) => console.error('Error loading patient details', err)
    });
  }

  viewLogDetails(patientLogId: number){
    this.router.navigateByUrl(`/patient-logs/${patientLogId}`)
  }
  
  openEditDialog(patient: Patient) {
    console.log(this.patient)
      const dialog = this.dialog.open(PatientFormComponent, {
        minWidth: '500px',
        data: {
          title: 'Edit patient',
          patient: this.patient
        }
      })
      dialog.afterClosed().subscribe({
        next: async result => {
          if (result) {
            await firstValueFrom(this.patientService.updatePatient(result.patient));
            this.getPatientDetails(patient.id)
            this.snackbarService.success("Updated patient record successfully");
          }
        }
      })
    }
    
    goBack(): void {
      this.location.back();
    }
}
