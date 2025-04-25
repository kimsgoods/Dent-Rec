import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientLogService } from '../../core/services/patient-log.service';
import { CommonModule, CurrencyPipe, Location } from '@angular/common';
import { PatientLogDetails } from '../../shared/models/patientLogDetails';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIcon } from '@angular/material/icon';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SnackbarService } from '../../core/services/snackbar.service';
import { UpdatePatientLogNotes } from '../../shared/models/patientLog';
import { PaymentService } from '../../core/services/payment.service';
import { PaymentFormComponent } from '../payment-form/payment-form.component';
import { NewPayment } from '../../shared/models/payment';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DialogService } from '../../core/services/dialog.service';

@Component({
  selector: 'app-patient-log-details',
  imports: [
    CommonModule,
    CurrencyPipe,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatChipsModule,
    MatIcon,
    ReactiveFormsModule,
    MatTooltipModule
  ],
  templateUrl: './patient-log-details.component.html',
  styleUrl: './patient-log-details.component.scss'
})
export class PatientLogDetailsComponent implements OnInit {
  patientLog!: PatientLogDetails;
  private route = inject(ActivatedRoute);
  private patientLogService = inject(PatientLogService);
  private paymentService = inject(PaymentService);
  private dialogService = inject(DialogService);
  private router = inject(Router);
  private location = inject(Location);
  private fb = inject(FormBuilder);
  private dialog = inject(MatDialog);
  private snackbar = inject(SnackbarService);

  totalPaid = 0;
  balance = 0;
  isEditingNotes = false;
  notesForm!: FormGroup;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadPatientLog(+id);
    }
  }

  loadPatientLog(id: number): void {
    this.patientLogService.getPatientLogById(id).subscribe({
      next: (data) => {
        this.patientLog = data;
        this.calculatePayments();
        this.initializeNotesForm();
      },
      error: (err) => console.error('Error loading log', err)
    });
  }

  calculatePayments(): void {
    this.totalPaid = this.patientLog?.payments?.reduce((sum, p) => sum + p.amount, 0) || 0;
    this.balance = (this.patientLog?.fee || 0) - this.totalPaid;
  }

  initializeNotesForm(): void {
    this.notesForm = this.fb.group({
      notes: [this.patientLog.notes || '']
    });
  }

  goBack(): void {
    this.location.back();
  }

  viewPatientDetails() : void {
    this.router.navigateByUrl(`/patients/${this.patientLog.patientId}`)
  }


  editNotes(): void {
    this.isEditingNotes = true;
  }

  saveNotes(): void {
    if (this.notesForm.valid) {
      const updatedNotes: UpdatePatientLogNotes = {
        id:this.patientLog.id,
        notes: this.notesForm.value.notes
      }
      this.patientLogService.updatePatientLog(updatedNotes).subscribe({
        next: () => {
          this.patientLog.notes = updatedNotes.notes;
          this.isEditingNotes = false;
          this.snackbar.success('Notes updated successfully');
        },
        error: (err) => {
          console.error('Error updating notes', err);
          this.snackbar.error('Failed to update notes');
        }
      });
    }
  }

  cancelEditNotes(): void {
    this.isEditingNotes = false;
    this.notesForm.patchValue({ notes: this.patientLog.notes });
  }

  async deletePayment(paymentId: number): Promise<void> {
    const confirmed = await this.dialogService.confirm(
      'Delete Payment',
      'Are you sure you want to delete this payment? This action cannot be undone.'
    );
  
    if (confirmed) {
      this.paymentService.deletePayment(paymentId).subscribe({
        next: () => {
          this.snackbar.success('Payment deleted successfully');
          this.loadPatientLog(this.patientLog.id);
        },
        error: (err) => {
          console.error('Error deleting payment', err);
          this.snackbar.error('Failed to delete payment');
        }
      });
    }
  }
  
  openAddPaymentDialog(): void {
    const dialogRef = this.dialog.open(PaymentFormComponent, {
      width: '400px',
      data: { 
        patientLogId: this.patientLog.id,
        balance: this.balance 
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const newPayment: NewPayment = {
          patientLogId: this.patientLog.id,
          patientId: this.patientLog.patientId,
          amount: result.amount,
          paymentMethod:result.paymentMethod
        }
        this.paymentService.createPayment(newPayment).subscribe({
          next: (result) => {
            this.calculatePayments();
            this.loadPatientLog(this.patientLog.id);
            this.snackbar.success('Payment added successfully');
          },
          error: (err) => {
            console.error('Error adding payment', err);
            this.snackbar.error('Failed to add payment');
          }
        });
      }
    });
  }
}