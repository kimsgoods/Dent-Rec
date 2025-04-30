import { Component, inject, OnInit, signal } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, of, startWith, debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { PatientLogService } from '../../core/services/patient-log.service';
import { PatientService } from '../../core/services/patient.service';
import { DentistService } from '../../core/services/dentist.service';
import { ProcedureService } from '../../core/services/procedure.service';
import { SnackbarService } from '../../core/services/snackbar.service';
import { PaymentService } from '../../core/services/payment.service';
import { PaginationParams } from '../../shared/models/paginationParams';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { PatientLogPatientComponent } from '../patient-log-patient/patient-log-patient.component';
import { PatientLogDentistComponent } from '../patient-log-dentist/patient-log-dentist.component';
import { PatientLogProcedureComponent } from '../patient-log-procedure/patient-log-procedure.component';
import { PatientLogPaymentComponent } from '../patient-log-payment/patient-log-payment.component';
import { PatientLogConfirmationComponent } from '../patient-log-confirmation/patient-log-confirmation.component';
import { PatientLogSummaryComponent } from '../patient-log-summary/patient-log-summary.component';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { Patient } from '../../shared/models/patient';
import { Dentist } from '../../shared/models/dentist';
import { Procedure, SelectedProcedure } from '../../shared/models/procedure';
import { NewPayment } from '../../shared/models/payment';

@Component({
  selector: 'app-patient-log-form',
  imports: [
    MatStepperModule,
    MatStepper,
    MatInputModule,
    PatientLogPatientComponent,
    PatientLogDentistComponent,
    PatientLogProcedureComponent,
    PatientLogPaymentComponent,
    PatientLogConfirmationComponent,
    PatientLogSummaryComponent,
    CommonModule,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './patient-log-form.component.html',
  styleUrl: './patient-log-form.component.scss'
})
export class PatientLogFormComponent implements OnInit {
  private patientLogService = inject(PatientLogService);
  private patientService = inject(PatientService);
  private dentistService = inject(DentistService);
  private procedureService = inject(ProcedureService);
  private paymentService = inject(PaymentService);
  private snackbarService = inject(SnackbarService);
  private router = inject(Router);
  patients: Patient[] = [];
  dentists: Dentist[] = [];
  procedures: Procedure[] = [];

  amountPaid: number = 0;
  paymentMethod: string = '';
  paymentFormValid = false;

  paginationParams = new PaginationParams();
  completionStatus = signal<{ patientComplete: boolean, dentistComplete: boolean, procedureComplete: boolean, paymentComplete: boolean }>
    ({ patientComplete: false, dentistComplete: false, procedureComplete: false, paymentComplete: false });

  patientSearchControl = new FormControl('');
  filteredPatients$: Observable<Patient[]> = of([]);

  selectedPatient: Patient | null = null;
  selectedDentist: Dentist | null = null;
  selectedProcedures: SelectedProcedure[] = [];
  notes: string = '';

  ngOnInit(): void {
    this.getProcedures();
    this.getDentists();

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

  handleDentistChange(event: boolean) {
    this.completionStatus.update(state => {
      state.dentistComplete = event;
      return state;
    })
  }

  handleProcedureChange(event: boolean) {
    this.completionStatus.update(state => {
      state.procedureComplete = event;
      return state;
    });
  
    const totalFee = this.getTotalFee() ?? 0;
    if (this.amountPaid !== totalFee) {
      this.amountPaid = totalFee;
    }
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

  getDentists() {
    this.dentistService.getDentists(this.paginationParams).subscribe({
      next: response => {
        this.dentists = response.data
      },
      error: error => console.log(error)
    })
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
    return this.selectedProcedures?.reduce((sum, item) => sum + (item.procedure.fee * item.quantity), 0);
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

  selectDentist(dentist: Dentist) {
    this.selectedDentist = dentist;
    this.handleDentistChange(true);
  }

  async createPatientLog(stepper: MatStepper) {
    const newLog = {
      patientId: this.selectedPatient?.id,
      dentistId: this.selectedDentist?.id,
      procedures: this.selectedProcedures.map(sp => ({
        id: sp.procedure.id,
        quantity: sp.quantity,
        notes: sp.notes
      })),
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
      paymentMethod: this.paymentMethod,
    }
    this.paymentService.createPayment(newPayment).subscribe({
      next: () => {
        this.router.navigateByUrl("/patient-logs");
      },
      error: error => this.snackbarService.error(error.message || "Something went wrong")
    })
  }

}