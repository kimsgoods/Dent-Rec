import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Procedure } from '../../shared/models/procedure';
import { MatListModule } from '@angular/material/list';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-log-procedure',
  imports: [
    MatListModule,
    CommonModule
  ],
  templateUrl: './patient-log-procedure.component.html',
  styleUrl: './patient-log-procedure.component.scss'
})
export class PatientLogProcedureComponent {
  @Input() procedures: Procedure[] = [];
  @Input() selectedProcedures: Procedure[] = [];
  @Output() selectedProceduresChange = new EventEmitter<Procedure[]>();
  @Output() procedureChange = new EventEmitter<boolean>();

  toggleSelection(procedure: Procedure) {
    const index = this.selectedProcedures.findIndex(p => p.id === procedure.id);
    if (index === -1) {
      this.selectedProcedures.push(procedure);
    } else {
      this.selectedProcedures.splice(index, 1);
    }
    this.selectedProceduresChange.emit(this.selectedProcedures);
    this.procedureChange.emit(this.selectedProcedures.length > 0);
  }
}
