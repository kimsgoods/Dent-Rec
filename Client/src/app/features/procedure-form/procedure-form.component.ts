import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { TextInputComponent } from '../../shared/components/text-input/text-input.component';
import { Procedure } from '../../shared/models/procedure';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { TextAreaComponent } from "../../shared/components/text-area/text-area.component";


@Component({
  selector: 'app-procedure-form',
  imports: [
    TextInputComponent,
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSelectModule,
    TextAreaComponent
  ],
  templateUrl: './procedure-form.component.html',
  styleUrl: './procedure-form.component.scss'
})

export class procedureFormComponent implements OnInit {
  procedureForm!: FormGroup;
  data = inject(MAT_DIALOG_DATA);
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<procedureFormComponent>);

  ngOnInit(): void {
    this.initializeForm();
    if(this.data.procedure) {
      this.procedureForm.reset(this.data.procedure)
    }
  }

  initializeForm() {

    this.procedureForm = this.fb.group({
      name: ['', [Validators.required]],
      description: [''],
      fee: [0],
    });

  }

  onSubmit(): void {
    if (this.procedureForm.valid) {
      let procedure: Procedure = this.procedureForm.value;
      if(this.data.procedure) procedure.id = this.data.procedure.id;
      this.dialogRef.close({
        procedure
      })
    }
  }
}
