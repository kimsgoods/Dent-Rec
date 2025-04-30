import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Patient } from '../../shared/models/patient';
import { SelectedProcedure } from '../../shared/models/procedure';

@Component({
  selector: 'app-payment-form',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ],
  templateUrl: './patient-log-payment.component.html',
  styleUrl: './patient-log-payment.component.scss'
})
export class PatientLogPaymentComponent implements OnInit {
  @Input() amountPaid!: number;
  @Input() paymentMethod!: string;
  @Input() selectedPatient!: Patient | null;
  @Input() selectedProcedures: SelectedProcedure[] = [];

  @Output() amountPaidChange = new EventEmitter<number>();
  @Output() paymentMethodChange = new EventEmitter<string>();
  @Output() paymentValid = new EventEmitter<boolean>();

  amountExceedsTotal = false;

  onAmountChange(value: number) {
    const totalFee = this.getTotalFee();
    const parsedValue = isNaN(value) || value < 0 ? 0 : value;

    this.amountExceedsTotal = parsedValue > totalFee;
    const validAmount = Math.min(parsedValue, totalFee);

    this.amountPaid = validAmount;
    this.amountPaidChange.emit(validAmount);
    this.emitValidity();
  }
  onpaymentMethodChange(value: string) {
    this.paymentMethod = value;
    this.paymentMethodChange.emit(value);
    this.emitValidity();
  }

  getTotalFee(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + (p.procedure.fee * p.quantity), 0);
  }

  getBalance(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + (p.procedure.fee * p.quantity), 0) - this.amountPaid;
  }

  emitValidity() {
    const totalFee = this.getTotalFee();
    const isAmountValid = this.amountPaid >= 0 && this.amountPaid <= totalFee;
    const ispaymentMethodValid = !!this.paymentMethod?.trim();
    const hasProcedures = this.selectedProcedures?.length > 0;

    const isValid = isAmountValid && ispaymentMethodValid && hasProcedures;
    this.paymentValid.emit(isValid);
  }

  ngOnInit(): void {
    this.emitValidity();
  }
}
