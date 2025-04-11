import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTooltip } from '@angular/material/tooltip';
import { MatIcon } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-custom-table',
  standalone: true,
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatButton,
    MatTooltip,
    MatIcon,
    FormsModule
  ],
  templateUrl: './custom-table.component.html',
  styleUrls: ['./custom-table.component.scss']
})
export class CustomTableComponent {
  @Input() columns: { field: string, header: string, pipe?: string, pipeArgs?: any, sortable?: boolean }[] = [];
  @Input() dataSource: any[] = [];
  @Input() actions: { label: string, icon: string, tooltip: string, action: (row: any) => void, disabled?: (row: any) => boolean }[] = [];
  @Input() totalItems: number = 0;
  @Input() pageSize: number = 50;
  @Input() pageIndex: number = 0;
  @Input() filter: string = "";
  @Input() title: string = "";
  @Input() clickEvent: () => void = () => { }
  @Input() defaultSortField: string = '';
  @Input() defaultSortDirection: 'asc' | 'desc' = 'asc';

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<{ field: string, direction: 'asc' | 'desc' }>();
  @Output() filterChange = new EventEmitter<string>();

  sortField: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  ngOnInit() {
    this.sortField = this.defaultSortField;
    this.sortDirection = this.defaultSortDirection;

    this.columns = this.columns.map(column => ({
      ...column,
      sortable: column.sortable !== false
    }));
  }

  onHeaderClick(column: any) {
    if (this.sortField === column.field) {
      // Toggle sort direction
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      // New column, start with ascending
      this.sortField = column.field;
      this.sortDirection = 'asc';
    }

    this.sortChange.emit({ field: this.sortField, direction: this.sortDirection });
  }
  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.pageChange.emit(event);
  }

  onSearchChange() {
    this.pageIndex = 1;
    this.filterChange.emit(this.filter);
  }

  onAction(action: (row: any) => void, row: any) {
    action(row);
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