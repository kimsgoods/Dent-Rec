import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { Patient } from '../../shared/models/patient';
import { CommonModule } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-patient-search',
  standalone:true,
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,    
    MatFormFieldModule,
    ReactiveFormsModule,
    MatAutocompleteModule
  ],
  templateUrl: './patient-search.component.html',
  styleUrl: './patient-search.component.scss'
})
export class PatientSearchComponent {
  @Input() patientSearchControl!: FormControl;
  @Input() filteredPatients!: Observable<Patient[]>;
  @Input() selectedPatient!: Patient | null;
  @Output() patientSelected = new EventEmitter<Patient>();

  displayPatient(patient: Patient): string {
    return patient ? `${patient.firstName} ${patient.lastName}` : '';
  }

  selectPatient(patient: Patient) {
    this.patientSelected.emit(patient);
  }
}
