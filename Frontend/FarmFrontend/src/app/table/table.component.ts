import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Sensor } from '../app-modules';
import { TableService } from './table.service';
import { formatDate } from '@angular/common';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {
  dataSource: MatTableDataSource<Sensor>;
  sensorData: Sensor[];

  sensorIdFilter: number | null = null;
  sensorTypeFilter: string | null = null;
  dateFilter: Date | null = null;
  exportType: string = 'json';

  isSortActive: boolean = false;
  isSortAsc: boolean = false;
  sortColumn: string = '';
  selectedFilters: string = '';

  displayedColumns = ['sensorId', 'sensorType', 'value', 'unit', 'timestamp'];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private service: TableService, private cdr: ChangeDetectorRef) {
    service.getSensors();
    service.data$.subscribe(data => {
      this.sensorData = data;
      this.setData();
    });
  }

  setData() {
    this.dataSource = new MatTableDataSource(this.sensorData);
    this.dataSource.paginator = this.paginator;
    this.cdr.markForCheck();
  }

  applyFilters() {
    let filters: string = "{";
    
    if (this.sensorIdFilter) {
      filters += `"SensorId": "${this.sensorIdFilter.toString()}"`;
    }
    if (this.sensorTypeFilter) {
      if (this.sensorIdFilter) {
        filters += ",";
      }
      filters += `"SensorType": "${this.sensorTypeFilter}"`;
    }
    if (this.dateFilter) {
      if (this.sensorTypeFilter || this.sensorIdFilter) { 
        filters += ",";
      }
      filters += `"Timestamp": "${formatDate(this.dateFilter, 'MM/dd/yyyy HH:mm:ss', 'en-US')}"`;
    }

    filters += "}";
    this.selectedFilters = filters;
    if (this.isSortActive) {
      const order = this.isSortAsc ? 'asc' : 'desc';
      this.service.getSensors(this.selectedFilters, this.sortColumn.charAt(0).toUpperCase() + this.sortColumn.slice(1), order);
    } else {
      this.service.getSensors(this.selectedFilters, "", "");
    }
  }

  clearFilters() {
    // Clear all filters
    this.sensorIdFilter = null;
    this.sensorTypeFilter = null;
    this.dateFilter = null;
    this.applyFilters();
  }

  applySort(column: string) {
    console.log(column);
    if ((!this.isSortActive && !this.isSortAsc) || column != this.sortColumn) {
      this.isSortActive = true;
      this.isSortAsc = true;
      this.sortColumn = column;
    }
    else if (this.isSortActive && this.isSortAsc) {
      this.isSortAsc = false;
    }
    else if (this.isSortActive && !this.isSortAsc) {
      this.isSortActive = false;
      this.sortColumn = '';
    }

    this.applyFilters();
  }

  export() {
    if (this.isSortActive) {
      const order = this.isSortAsc ? 'asc' : 'desc';
      this.service.getSensorsFile(this.selectedFilters, this.sortColumn.charAt(0).toUpperCase() + this.sortColumn.slice(1), order, this.exportType).subscribe((data: Blob) => {
        const fileName = `downloadedFile.${this.exportType === 'csv' ? 'csv' : 'json'}`;
        saveAs(data, fileName);
      });
    } else {
      this.service.getSensorsFile(this.selectedFilters, "", "", this.exportType).subscribe((data: Blob) => {
        const fileName = `downloadedFile.${this.exportType === 'csv' ? 'csv' : 'json'}`;
        saveAs(data, fileName);
      });
    }
  }

}
