import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {SensorService} from "../../service/sensor.service";
import {interval, startWith, Subject, switchMap, takeUntil} from "rxjs";
import {SensorStats} from "../../types/sensor-types";

@Component({
  selector: 'app-avg-sensor',
  templateUrl: './avg-sensor.component.html',
  styleUrl: './avg-sensor.component.css'
})
export class AvgSensorComponent implements OnInit, OnDestroy {
  @Input() sensorId: number;
  public sensorStats: SensorStats = {
    avg: 0,
    lastValue: 0
  }

  private destroy$ = new Subject<void>();

  constructor(private sensorService: SensorService) {}

  ngOnInit() {
    this.startPolling()
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private startPolling() {
    interval(500)
      .pipe(
        startWith(0),
        takeUntil(this.destroy$),
        switchMap(() => this.sensorService.getSensorAverageData(this.sensorId))
      )
      .subscribe({
        next: value => {
          this.sensorStats = value;
        },
        error: error => {
          console.error('Error polling sensor data:', error);
        },
      });
  }
}
