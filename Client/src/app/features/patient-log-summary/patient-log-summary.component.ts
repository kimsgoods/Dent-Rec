import { Component, inject, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Patient } from '../../shared/models/patient';
import { SelectedProcedure } from '../../shared/models/procedure';
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
  @Input() selectedProcedures: SelectedProcedure[] = [];
  @Input() amountPaid: number = 0;
  @Input() paymentMethod: string = '';  
  currentDate = Date.now();

  getTotalFee(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + (p.procedure.fee * p.quantity), 0);
  }
}
