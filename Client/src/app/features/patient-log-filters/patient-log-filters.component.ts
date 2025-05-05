import { Component, inject, OnInit } from '@angular/core';
import { PatientLogService } from '../../core/services/patient-log.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDatepickerModule, MatDateRangeInput } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatRadioModule } from '@angular/material/radio';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-log-filters',
  imports: [
    MatRadioModule,
    MatButton,
    FormsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatDateRangeInput,
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [
    provideNativeDateAdapter()
  ],
  templateUrl: './patient-log-filters.component.html',
  styleUrl: './patient-log-filters.component.scss'
})
export class PatientLogFiltersComponent implements OnInit {
  patientLogService = inject(PatientLogService);
  private dialogRef = inject(MatDialogRef<PatientLogFiltersComponent>);
  data = inject(MAT_DIALOG_DATA);

  paymentStatus: string[] = ["All", "Paid", "Partial", "Pending"]
  selectedPaymentFilter: string = this.data.selectedPaymentFilter;

  dateRangeForm = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null)
  });


  applyFilters() {
    const result = {
      selectedPaymentFilter: this.selectedPaymentFilter,
      dateRange: {
        start: this.dateRangeForm.value.start,
        end: this.dateRangeForm.value.end
      }
    };
    this.dialogRef.close(result);
  }

  clearFilters() {
    this.selectedPaymentFilter = 'All';
    this.dateRangeForm.reset();
  }

  ngOnInit(): void {
    if (this.data) {
      this.selectedPaymentFilter = this.data.selectedPaymentFilter || 'All';
      
      if (this.data.dateRange) {
        this.dateRangeForm.patchValue({
          start: this.data.dateRange.start ? new Date(this.data.dateRange.start) : null,
          end: this.data.dateRange.end ? new Date(this.data.dateRange.end) : null
        });
      }
    }
  }
}
