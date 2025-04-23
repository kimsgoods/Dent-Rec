import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Dentist } from '../../shared/models/dentist';
import { MatRadioModule } from '@angular/material/radio';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-log-dentist',
  imports: [
    MatRadioModule,
    CommonModule
  ],
  templateUrl: './patient-log-dentist.component.html',
  styleUrl: './patient-log-dentist.component.scss'
})
export class PatientLogDentistComponent {
  @Input() selectedDentist!: Dentist | null;
  @Input() dentists: Dentist[] = [];


  @Output() dentistSelected = new EventEmitter<Dentist>();

  selectDentist(dentist: Dentist) {
    this.dentistSelected.emit(dentist);
  }
}
