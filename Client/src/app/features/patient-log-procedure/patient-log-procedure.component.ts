import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Procedure, SelectedProcedure } from '../../shared/models/procedure';
import { MatListModule } from '@angular/material/list';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-patient-log-procedure',
  standalone: true,
  imports: [
    MatListModule,
    CommonModule,
    FormsModule
  ],
  templateUrl: './patient-log-procedure.component.html',
  styleUrl: './patient-log-procedure.component.scss'
})
export class PatientLogProcedureComponent {
  @Input() procedures: Procedure[] = [];
  @Input() selectedProcedures: SelectedProcedure[] = [];
  @Output() selectedProceduresChange = new EventEmitter<SelectedProcedure[]>();
  @Output() procedureChange = new EventEmitter<boolean>();

  // Teeth data - number, name, and position for display
  teethChart = [
    { number: '1', name: 'Third Molar (Wisdom Tooth)' },
    { number: '2', name: 'Second Molar' },
    { number: '3', name: 'First Molar' },
    { number: '4', name: 'Second Premolar (Bicuspid)' },
    { number: '5', name: 'First Premolar (Bicuspid)' },
    { number: '6', name: 'Canine (Cuspid)' },
    { number: '7', name: 'Lateral Incisor' },
    { number: '8', name: 'Central Incisor' },
    // Lower teeth (continued numbering)
    { number: '9', name: 'Central Incisor' },
    { number: '10', name: 'Lateral Incisor' },
    { number: '11', name: 'Canine (Cuspid)' },
    { number: '12', name: 'First Premolar (Bicuspid)' },
    { number: '13', name: 'Second Premolar (Bicuspid)' },
    { number: '14', name: 'First Molar' },
    { number: '15', name: 'Second Molar' },
    { number: '16', name: 'Third Molar (Wisdom Tooth)' },
    // Upper right quadrant
    { number: '17', name: 'Third Molar (Wisdom Tooth)' },
    { number: '18', name: 'Second Molar' },
    { number: '19', name: 'First Molar' },
    { number: '20', name: 'Second Premolar (Bicuspid)' },
    { number: '21', name: 'First Premolar (Bicuspid)' },
    { number: '22', name: 'Canine (Cuspid)' },
    { number: '23', name: 'Lateral Incisor' },
    { number: '24', name: 'Central Incisor' },
    // Lower right quadrant
    { number: '25', name: 'Central Incisor' },
    { number: '26', name: 'Lateral Incisor' },
    { number: '27', name: 'Canine (Cuspid)' },
    { number: '28', name: 'First Premolar (Bicuspid)' },
    { number: '29', name: 'Second Premolar (Bicuspid)' },
    { number: '30', name: 'First Molar' },
    { number: '31', name: 'Second Molar' },
    { number: '32', name: 'Third Molar (Wisdom Tooth)' }
  ];

  selectedTeeth: string[] = [];
  showTeethChartForProcedure: number | null = null;
  temporarilyChecked: number | null = null;

  toggleSelection(procedure: Procedure) {
    if (this.isSelected(procedure)) {
      const index = this.selectedProcedures.findIndex(sp => sp.procedure.id === procedure.id);
      this.selectedProcedures.splice(index, 1);
      this.emitChanges();
    } else {
      if (procedure.pricingType === 'PerTooth') {
        this.temporarilyChecked = procedure.id;
        this.showTeethChartForProcedure = procedure.id;
      } else {
        this.selectedProcedures.push({
          procedure,
          quantity: 1,
          notes: ''
        });
        this.emitChanges();
      }
    }
  }

  toggleToothSelection(toothNumber: string) {
    const index = this.selectedTeeth.indexOf(toothNumber);
    if (index === -1) {
      this.selectedTeeth.push(toothNumber);
    } else {
      this.selectedTeeth.splice(index, 1);
    }
  }

  confirmTeethSelection() {
    if (this.showTeethChartForProcedure && this.selectedTeeth.length > 0) {
      const procedure = this.procedures.find(p => p.id === this.showTeethChartForProcedure);
      if (procedure) {
        this.selectedProcedures.push({
          procedure,
          quantity: this.selectedTeeth.length,
          notes: this.selectedTeeth.join(', ')
        });
        this.emitChanges();
      }
    }
    this.showTeethChartForProcedure = null;
    this.selectedTeeth = [];
  } 

  cancelTeethSelection() {
    this.temporarilyChecked = null;
    this.showTeethChartForProcedure = null;
    this.selectedTeeth = [];
  }

  updateCheckboxState(procedure: Procedure): boolean {
    return this.isSelected(procedure) || this.temporarilyChecked === procedure.id;
  }

  isSelected(procedure: Procedure): boolean {
    return this.selectedProcedures.some(sp => sp.procedure.id === procedure.id);
  }

  isToothSelected(toothNumber: string): boolean {
    return this.selectedTeeth.includes(toothNumber);
  }

  getSelectedTeethForProcedure(procedureId: number): string {
    const selected = this.selectedProcedures.find(sp => sp.procedure.id === procedureId);
    return selected?.notes || '';
  }

  private emitChanges() {
    this.selectedProceduresChange.emit(this.selectedProcedures);
    this.procedureChange.emit(this.selectedProcedures.length > 0);
  }
}