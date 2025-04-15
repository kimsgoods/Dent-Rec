import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-payment-form',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './payment-form.component.html',
  styleUrl: './payment-form.component.scss'
})
export class PaymentFormComponent {
  paymentForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<PaymentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { balance: number }
  ) {
    this.paymentForm = this.fb.group({
      amount: [null, [Validators.required, Validators.max(this.data.balance)]],
      paymentMethod: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.paymentForm.valid) {
      this.dialogRef.close(this.paymentForm.value);
    }
  }
}
