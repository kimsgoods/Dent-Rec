import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { TextInputComponent } from '../../shared/components/text-input/text-input.component';
import { Dentist } from '../../shared/models/dentist';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';


@Component({
  selector: 'app-dentist-form',
  imports: [
    TextInputComponent,
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './dentist-form.component.html',
  styleUrl: './dentist-form.component.scss'
})

export class DentistFormComponent implements OnInit {
  dentistForm!: FormGroup;
  data = inject(MAT_DIALOG_DATA);
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<DentistFormComponent>);
 
  genders: string[] = ["Male", "Female"];

  ngOnInit(): void {
    this.initializeForm();
    if (this.data.dentist) {
      this.dentistForm.reset(this.data.dentist)
    }
  }

  initializeForm() {

    this.dentistForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phone: ['', [Validators.pattern(/^[0-9-]*$/)]],
      email: [''],
    });

  }

  onSubmit(): void {
    if (this.dentistForm.valid) {
      let dentist: Dentist = this.dentistForm.value;
      if (this.data.dentist) dentist.id = this.data.dentist.id;
      this.dialogRef.close({
        dentist
      })
    }
  }  
}
