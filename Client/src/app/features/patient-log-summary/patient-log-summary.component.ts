import { Component, inject, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Patient } from '../../shared/models/patient';
import { Procedure } from '../../shared/models/procedure';
import { CommonModule } from '@angular/common';
import { Dentist } from '../../shared/models/dentist';

@Component({
  selector: 'app-patient-log-summary',
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './patient-log-summary.component.html',
  styleUrl: './patient-log-summary.component.scss'
})
export class PatientLogSummaryComponent {
  @Input() selectedPatient: Patient | null = null;
  @Input() selectedDentist: Dentist | null = null;
  @Input() selectedProcedures: Procedure[] = [];
  @Input() amountPaid: number = 0;
  @Input() paymentType: string = '';

  getTotalFee(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + p.fee, 0);
  }
}
