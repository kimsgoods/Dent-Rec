import { Component, inject, OnInit } from '@angular/core';
import { CustomTableComponent } from '../../shared/components/custom-table/custom-table.component';
import { PaginationParams } from '../../shared/models/paginationParams';
import { Router } from '@angular/router';
import { dentistService } from '../../core/services/dentist.service';
import { MatDialog } from '@angular/material/dialog';
import { Dentist } from '../../shared/models/dentist';
import { PageEvent } from '@angular/material/paginator';
import { DentistFormComponent } from '../dentist-form/dentist-form.component';
import { firstValueFrom } from 'rxjs';
import { DialogService } from '../../core/services/dialog.service';

@Component({
  selector: 'app-dentists',
  imports: [
    CustomTableComponent
  ],
  templateUrl: './dentists.component.html',
  styleUrl: './dentists.component.scss'
})
export class DentistsComponent {
  private dentistService = inject(dentistService);
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  paginationParams = new PaginationParams();
  totalItems = 0;
  dentists: Dentist[] = [];
  title = "Dentist Records"

  getdentists() {
    this.dentistService.getDentists(this.paginationParams).subscribe({
      next: response => {
          this.dentists = response.data,
          this.totalItems = response.count
      },
      error: error => console.log(error)
    });
  }

  ngOnInit(): void {
    this.paginationParams.orderBy = "firstName asc"
    this.getdentists();
  }

  onPageChange(event: PageEvent) {
    this.paginationParams.page = event.pageIndex + 1;
    this.paginationParams.pageSize = event.pageSize;
    this.getdentists();
  }

  onSortChange(event: { field: string, direction: 'asc' | 'desc' }) {
    this.paginationParams.orderBy = `${event.field} ${event.direction}`;
    this.getdentists();
  }

  columns = [
    { field: 'firstName', header: 'First Name' },
    { field: 'lastName', header: 'Last Name' },
    { field: 'phone', header: 'Phone' },
    { field: 'email', header: 'Email' },
  ]

  actions = [       
    {
      label: 'Edit',
      icon: 'edit',
      tooltip: 'Edit dentist',
      action: (row: any) => {
        this.openEditDialog(row)
      }
    },
    {
      label: 'Delete',
      icon: 'delete',
      tooltip: 'Delete dentist',
      action: (row: any) => {
        this.openConfirmDialog(row.id)
      }
    }    
  ];

  onAction(action: (row: any) => void, row: any) {
    action(row);
  }

  openCreateDialog() {
    const dialog = this.dialog.open(DentistFormComponent, {
      minWidth: '500px',
      data: {
        title: 'Create dentist'
      }
    });
    dialog.afterClosed().subscribe({
      next: async result => {
        if (result) {
          const dentist = await firstValueFrom(this.dentistService.createDentist(result.dentist));
          if (dentist) {
            this.dentists.push(dentist);
            this.getdentists();
          }
        }
      }
    })
  }  

  openEditDialog(dentist: Dentist) {
    const dialog = this.dialog.open(DentistFormComponent, {
      minWidth: '500px',
      data: {
        title: 'Edit dentist',
        dentist
      }
    })
    dialog.afterClosed().subscribe({
      next: async result => {
        if (result) {
          await firstValueFrom(this.dentistService.updateDentist(result.dentist));
          const index = this.dentists.findIndex(p => p.id === result.dentist.id);
          if (index !== -1) {
            this.dentists[index] = result.dentist;
          }          
          this.getdentists();
        }
      }
    })
  }

  async openConfirmDialog(id: number) {
    const confirmed = await this.dialogService.confirm(
      'Confirm delete dentist',
      'Are you sure you want to delete this dentist? This cannot be undone'
    );
    if (confirmed) this.onDelete(id);
  }

  onDelete(id: number) {
    this.dentistService.deleteDentist(id).subscribe({
      next: () => {
        this.dentists = this.dentists.filter(x => x.id !== id);
        this.getdentists();
      }
    })
  }

}
