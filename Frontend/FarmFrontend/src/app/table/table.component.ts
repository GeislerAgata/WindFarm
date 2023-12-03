import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Sensor } from '../app-modules';
import { TableService } from './table.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {
  dataSource: MatTableDataSource<Sensor>;
  filteredDataSource: MatTableDataSource<Sensor>;
  sensorData: Sensor[];

  sensorIdFilter: number | null = null;
  sensorTypeFilter: string | null = null;
  dateFilter: Date | null = null;
  exportType: string = 'json';

  isSortActive: boolean = false;
  isSortAsc: boolean = false;
  sortColumn: string = '';

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
      if (this.sensorIdFilter) filters += ",";
      filters += `"SensorType": "${this.sensorTypeFilter}"`;
    }
    if (this.dateFilter) {
      if (this.sensorTypeFilter) filters += ",";
      filters += `"Timestamp": "${formatDate(this.dateFilter, 'MM/dd/yyyy HH:mm:ss', 'en-US')}"`;
    }

    filters += "}";
    if (this.isSortActive) {
      const order = this.isSortAsc ? 'asc' : 'desc';
      this.service.getSensors(filters.toString(), this.sortColumn.charAt(0).toUpperCase() + this.sortColumn.slice(1), order);
    } else {
      this.service.getSensors(filters.toString(), "", "");
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

  //change to backend returning value
  export() {
    if (this.exportType == 'json') {
      const jsonContent = JSON.stringify(this.dataSource.data, null, 2);

      // Create a Blob containing the JSON data
      const blob = new Blob([jsonContent], { type: 'application/json' });

      // Create a link element and trigger a download
      const a = document.createElement('a');
      a.href = window.URL.createObjectURL(blob);
      a.download = 'exported_sensor_data.json';
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
    }
    else if (this.exportType == 'csv') {
      const csvContent = 'data:text/csv;charset=utf-8,' +
      this.dataSource.data.map((row) => Object.values(row).join(',')).join('\n');

      const encodedUri = encodeURI(csvContent);
      const link = document.createElement('a');
      link.setAttribute('href', encodedUri);
      link.setAttribute('download', 'exported_sensor_data.csv');
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  }

}
