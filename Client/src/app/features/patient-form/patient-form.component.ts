import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { TextInputComponent } from '../../shared/components/text-input/text-input.component';
import { Patient } from '../../shared/models/patient';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { SelectInputComponent } from '../../shared/components/select-input/select-input.component';


@Component({
  selector: 'app-patient-form',
  imports: [
    TextInputComponent,
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSelectModule,
    SelectInputComponent
  ],
  templateUrl: './patient-form.component.html',
  styleUrl: './patient-form.component.scss'
})

export class PatientFormComponent implements OnInit {
  patientForm!: FormGroup;
  data = inject(MAT_DIALOG_DATA);
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<PatientFormComponent>);
 
  genders: string[] = ["Male", "Female"];

  ngOnInit(): void {
    this.initializeForm();
    if (this.data.patient) {
      this.patientForm.reset(this.data.patient)
    }
  }

  initializeForm() {

    this.patientForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      age: [0, [Validators.required, Validators.min(0)]],
      gender: ['', [Validators.required]],
      address: [''],
      phone: [''],
      email: [''],
    });

  }

  onSubmit(): void {
    if (this.patientForm.valid) {
      let patient: Patient = this.patientForm.value;
      if (this.data.patient) patient.id = this.data.patient.id;
      this.dialogRef.close({
        patient
      })
    }
  }  
}
