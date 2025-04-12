import { Component, inject, OnInit } from '@angular/core';
import { CustomTableComponent } from '../../shared/components/custom-table/custom-table.component';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Router } from '@angular/router';
import { ProcedureService } from '../../core/services/procedure.service';
import { MatDialog } from '@angular/material/dialog';
import { Procedure } from '../../shared/models/procedure';
import { PageEvent } from '@angular/material/paginator';
import { procedureFormComponent as ProcedureFormComponent } from '../procedure-form/procedure-form.component';
import { firstValueFrom } from 'rxjs';
import { DialogService } from '../../core/services/dialog.service';
import { SnackbarService } from '../../core/services/snackbar.service';

@Component({
  selector: 'app-procedures',
  imports: [
    CustomTableComponent
  ],
  templateUrl: './procedures.component.html',
  styleUrl: './procedures.component.scss'
})
export class ProceduresComponent {
  private procedureService = inject(ProcedureService);
  private dialogService = inject(DialogService);
  private snackbarService = inject(SnackbarService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  paginationParams = new PaginationParams();
  totalItems = 0;
  procedures: Procedure[] = [];
  title = "Procedure Records"
  defaultSortField = "name"
  defaultSortDirection: "asc" | "desc" = "asc"

  getProcedures() {
    this.procedureService.getProcedures(this.paginationParams).subscribe({
      next: response => {
        this.procedures = response.data,
          this.totalItems = response.count
      },
      error: error => console.log(error)
    });
  }

  ngOnInit(): void {
    this.paginationParams.orderBy = `${this.defaultSortField} ${this.defaultSortDirection}`
    this.getProcedures();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getProcedures();
  }

  onSortChange(event: { field: string, direction: 'asc' | 'desc' }) {
    this.paginationParams.orderBy = `${event.field} ${event.direction}`;
    this.getProcedures();
  }

  columns = [
    { field: 'name', header: 'Procedure Name' },
    { field: 'description', header: 'description' },
    { field: 'fee', header: 'Fee', pipe: 'currency', pipeArgs: 'PHP' }
  ]

  actions = [
    {
      label: 'Edit',
      icon: 'edit',
      tooltip: 'Edit procedure',
      action: (row: any) => {
        this.openEditDialog(row)
      }
    },
    {
      label: 'Delete',
      icon: 'delete',
      tooltip: 'Delete procedure',
      action: (row: any) => {
        this.openConfirmDialog(row.id)
      }
    }
  ];

  onAction(action: (row: any) => void, row: any) {
    action(row);
  }

  openCreateDialog() {
    const dialog = this.dialog.open(ProcedureFormComponent, {
      minWidth: '500px',
      data: {
        title: 'Create Procedure'
      }
    });
    dialog.afterClosed().subscribe({
      next: async result => {
        if (result) {
          const procedure = await firstValueFrom(this.procedureService.createProcedure(result.procedure));
          if (procedure) {
            this.procedures.push(procedure);
            this.getProcedures();            
            this.snackbarService.success("Created new procedure record successfully");
          }
        }
      }
    })
  }

  openEditDialog(procedure: Procedure) {
    const dialog = this.dialog.open(ProcedureFormComponent, {
      minWidth: '500px',
      data: {
        title: 'Edit procedure',
        procedure
      }
    })
    dialog.afterClosed().subscribe({
      next: async result => {
        if (result) {
          await firstValueFrom(this.procedureService.updateProcedure(result.procedure));
          const index = this.procedures.findIndex(p => p.id === result.procedure.id);
          if (index !== -1) {
            this.procedures[index] = result.procedure;
          }
          this.getProcedures();
          
          this.snackbarService.success("Updated procedure record successfully");
        }
      }
    })
  }

  async openConfirmDialog(id: number) {
    const confirmed = await this.dialogService.confirm(
      'Confirm delete procedure',
      'Are you sure you want to delete this procedure? This cannot be undone'
    );
    if (confirmed) this.onDelete(id);
  }

  onDelete(id: number) {
    this.procedureService.deleteProcedure(id).subscribe({
      next: () => {
        this.procedures = this.procedures.filter(x => x.id !== id);
        this.getProcedures();
        
        this.snackbarService.success("Deleted procedure record successfully");
      }
    })
  }

}
