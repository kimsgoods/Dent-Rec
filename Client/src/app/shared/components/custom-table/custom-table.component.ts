import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTooltip } from '@angular/material/tooltip';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-custom-table',
  standalone: true,
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatButton,
    MatTooltip,
    MatIcon
  ],
  templateUrl: './custom-table.component.html',
  styleUrls: ['./custom-table.component.scss']
})
export class CustomTableComponent {
  @Input() columns: { field: string, header: string, pipe?: string, pipeArgs?: any }[] = [];
  @Input() dataSource: any[] = [];
  @Input() actions: { label: string, icon: string, tooltip: string, action: (row: any) => void, disabled?: (row: any) => boolean }[] = [];
  @Input() totalItems: number = 0;
  @Input() pageSize: number = 50;
  @Input() pageIndex: number = 0;
  @Input() title: string = "";
  @Input() clickEvent: () => void = () => { }

  @Output() pageChange = new EventEmitter<PageEvent>();

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.pageChange.emit(event);
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