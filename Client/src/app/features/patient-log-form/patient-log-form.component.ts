import { Component, inject, OnInit, signal } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, of, startWith, debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { PatientLogService } from '../../core/services/patient-log.service';
import { PatientService } from '../../core/services/patient.service';
import { ProcedureService } from '../../core/services/procedure.service';
import { SnackbarService } from '../../core/services/snackbar.service';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Patient } from '../../shared/models/patient';
import { Procedure } from '../../shared/models/procedure';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { PatientLogPatientComponent } from '../patient-log-patient/patient-log-patient.component';
import { PatientLogProcedureComponent } from '../patient-log-procedure/patient-log-procedure.component';
import { PatientLogPaymentComponent } from '../patient-log-payment/patient-log-payment.component';
import { PatientLogConfirmationComponent } from '../patient-log-confirmation/patient-log-confirmation.component';
import { PatientLogSummaryComponent } from '../patient-log-summary/patient-log-summary.component';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { NewPayment } from '../../shared/models/payment';
import { PaymentService } from '../../core/services/payment.service';
@Component({
  selector: 'app-patient-log-form',
  imports: [
    MatStepperModule,
    MatStepper,
    MatInputModule,
    PatientLogPatientComponent,
    PatientLogProcedureComponent,
    PatientLogPaymentComponent,
    PatientLogConfirmationComponent,
    PatientLogSummaryComponent,
    CommonModule
  ],
  templateUrl: './patient-log-form.component.html',
  styleUrl: './patient-log-form.component.scss'
})
export class PatientLogFormComponent implements OnInit {
  private patientLogService = inject(PatientLogService);
  private patientService = inject(PatientService);
  private procedureService = inject(ProcedureService);
  private paymentService = inject(PaymentService);
  private snackbarService = inject(SnackbarService);
  private router = inject(Router);
  patients: Patient[] = [];
  selectedPatient: Patient | null = null;
  procedures: Procedure[] = [];
  selectedProcedures: Procedure[] = [];
  paginationParams = new PaginationParams();
  completionStatus = signal<{ patientComplete: boolean, procedureComplete: boolean, paymentComplete: boolean }>({ patientComplete: false, procedureComplete: false, paymentComplete: false });
  amountPaid: number = 0;
  paymentType: string = '';
  notes: string = '';
  paymentFormValid = false;

  patientSearchControl = new FormControl('');
  filteredPatients$: Observable<Patient[]> = of([]);

  ngOnInit(): void {
    this.getProcedures();

    this.filteredPatients$ = this.patientSearchControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => this.searchPatients(value))
    );
  }

  handlePatientChange(event: boolean) {
    this.completionStatus.update(state => {
      state.patientComplete = event;
      return state;
    })
  }

  handleProcedureChange(event: boolean) {
    this.completionStatus.update(state => {
      state.procedureComplete = event;
      return state;
    });

    // Set default amountPaid to total fee
    this.amountPaid = this.getTotalFee() ?? 0;
  }

  handlePaymentChange(isValid: boolean) {
    this.paymentFormValid = isValid;
    this.completionStatus.update(state => {
      state.paymentComplete = isValid;
      return state;
    })
  }

  getPatients() {
    this.patientService.getPatients(this.paginationParams).subscribe({
      next: response => {
        this.patients = response.data
      },
      error: error => console.log(error)
    });
  }

  getProcedures() {
    this.procedureService.getProcedures(this.paginationParams).subscribe({
      next: response => {
        this.procedures = response.data
      },
      error: error => console.log(error)
    });
  }

  getTotalFee() {
    return this.selectedProcedures?.reduce((sum, procedure) => sum + procedure.fee, 0);
  }

  displayPatient(patient: Patient): string {
    return patient ? `${patient.firstName} ${patient.lastName}` : '';
  }

  searchPatients(filter: string | null): Observable<Patient[]> {
    if (!filter) return of([]);
    const paginationParams = new PaginationParams();
    paginationParams.filter = filter;
    paginationParams.pageSize = 10;
    paginationParams.page = 1;

    return this.patientService.getPatients(paginationParams).pipe(
      // Map only the data (not paging info)
      switchMap(res => of(res.data))
    );
  }

  selectPatient(patient: Patient) {
    this.selectedPatient = patient;
    this.handlePatientChange(true);
  }

  async createPatientLog(stepper: MatStepper) {
    const newLog = {
      patientId: this.selectedPatient?.id,
      dentistId: 1,
      procedureIds: this.selectedProcedures?.map(p => p.id),
      notes: this.notes
    };

    this.patientLogService.createPatientLog(newLog).subscribe({
      next: (result) => {
        this.snackbarService.success("Created new Patient Log successfully")
        stepper.reset();
        this.createPayment(result);
      },
      error: error => this.snackbarService.error(error.message || "Something went wrong")
    });

  }

  async createPayment(patientLogId: number) {
    const newPayment: NewPayment = {
      amount: this.amountPaid,
      patientId: this.selectedPatient != null ? this.selectedPatient.id : 0,
      patientLogId: patientLogId,
      paymentMethod: this.paymentType,
    }
    this.paymentService.createPayment(newPayment).subscribe({
      next: () =>{        
        this.router.navigateByUrl("/patient-logs");
      },
      error: error => this.snackbarService.error(error.message || "Something went wrong")
    })
  }

}