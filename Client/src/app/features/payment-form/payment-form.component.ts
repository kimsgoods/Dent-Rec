import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Patient } from '../../shared/models/patient';
import { Procedure } from '../../shared/models/procedure';

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
  templateUrl: './payment-form.component.html',
  styleUrl: './payment-form.component.scss'
})
export class PaymentFormComponent {
  @Input() amountPaid!: number;
  @Input() paymentType!: string;
  @Input() selectedPatient!: Patient | null;
  @Input() selectedProcedures: Procedure[] = [];

  @Output() amountPaidChange = new EventEmitter<number>();
  @Output() paymentTypeChange = new EventEmitter<string>();
  @Output() paymentChanged = new EventEmitter<boolean>();
  @Output() paymentValid = new EventEmitter<boolean>();

  amountExceedsTotal = false;

  onAmountChange(value: number) {
    const totalFee = this.getTotalFee();
    const validAmount = Math.min(value, totalFee);
    this.amountExceedsTotal = value > totalFee;

    this.amountPaid = validAmount;
    this.amountPaidChange.emit(validAmount);
    this.paymentChanged.emit(!!validAmount);
    this.emitValidity();
  }

  onPaymentTypeChange(value: string) {
    this.paymentTypeChange.emit(value);
    this.paymentChanged.emit(!!value);
    this.emitValidity();
  }

  getTotalFee(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + p.fee, 0);
  }

  getBalance(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + p.fee, 0) - this.amountPaid;
  }

  emitValidity() {
    const isValid = this.amountPaid >= 0 &&
      this.amountPaid <= this.getTotalFee() &&
      !!this.paymentType;
    this.paymentValid.emit(isValid);
  }
}
