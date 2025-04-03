import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-custom-table',
  standalone: true,
  imports: [
    CommonModule,
    MatPaginatorModule,
    RouterLink
  ],
  templateUrl: './custom-table.component.html',
  styleUrls: ['./custom-table.component.scss']
})
export class CustomTableComponent {
  @Input() columns: { field: string, header: string, pipe?: string, pipeArgs?: any }[] = [];
  @Input() dataSource: any[] = [];
  @Input() totalItems: number = 0; 
  @Input() pageSize: number = 50; 
  @Input() pageIndex: number = 0; 

  @Output() pageChange = new EventEmitter<PageEvent>();

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.pageChange.emit(event);
  }

  getCellValue(row: any, column: any) {
    const value = row[column.field];
    if (column.pipe === 'currency') {
      return new Intl.NumberFormat('en-PH', { style: 'currency', currency: column.pipeArgs || 'PHP' }).format(value);
    }
    if (column.pipe === 'date') {
      return new Date(value).toLocaleString('en-PH', column.pipeArgs);
    }
    return value;
  }
}