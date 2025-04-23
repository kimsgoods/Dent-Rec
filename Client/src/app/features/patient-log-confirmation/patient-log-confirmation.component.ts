import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Patient } from '../../shared/models/patient';
import { Procedure } from '../../shared/models/procedure';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Dentist } from '../../shared/models/dentist';

@Component({
  selector: 'app-patient-log-confirmation',
  imports: [
    CommonModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule
],
  templateUrl: './patient-log-confirmation.component.html',
  styleUrl: './patient-log-confirmation.component.scss'
})
export class PatientLogConfirmationComponent {
  @Input() selectedPatient!: Patient | null;  
  @Input() selectedDentist!: Dentist | null;
  @Input() selectedProcedures: Procedure[] = [];
  @Input() amountPaid!: number;
  @Input() paymentType!: string;
  @Input() notes: string = '';
  currentDate = Date.now();

  @Output() notesChange = new EventEmitter<string>();

  getTotalFee(): number {
    return this.selectedProcedures.reduce((total, p) => total + p.fee, 0);
  }
}
