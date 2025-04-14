import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
export class PaymentFormComponent implements OnInit{
  @Input() amountPaid!: number;
  @Input() paymentType!: string;
  @Input() selectedPatient!: Patient | null;
  @Input() selectedProcedures: Procedure[] = [];

  @Output() amountPaidChange = new EventEmitter<number>();
  @Output() paymentTypeChange = new EventEmitter<string>();
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
  onPaymentTypeChange(value: string) {
    this.paymentType = value;
    this.paymentTypeChange.emit(value);
    this.emitValidity();
  }

  getTotalFee(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + p.fee, 0);
  }

  getBalance(): number {
    return this.selectedProcedures?.reduce((sum, p) => sum + p.fee, 0) - this.amountPaid;
  }

  emitValidity() {
    const totalFee = this.getTotalFee();
    const isAmountValid = this.amountPaid >= 0 && this.amountPaid <= totalFee;
    const isPaymentTypeValid = !!this.paymentType?.trim();
    const hasProcedures = this.selectedProcedures?.length > 0;
  
    const isValid = isAmountValid && isPaymentTypeValid && hasProcedures;
    this.paymentValid.emit(isValid);
  }

  ngOnInit(): void {
    this.emitValidity();
  }
}
