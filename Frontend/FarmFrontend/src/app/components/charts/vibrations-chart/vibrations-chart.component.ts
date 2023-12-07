import { Component } from '@angular/core';
import {SensorService} from "../../../service/sensor.service";

@Component({
  selector: 'app-vibrations-chart',
  templateUrl: './vibrations-chart.component.html',
  styleUrl: './vibrations-chart.component.css'
})
export class VibrationsChartComponent {
  public vibrationData: { x: Date, y: number }[] = [];

  public vibrationChart: any

  public vibrationChartOptions = {
    title: {
      text: "Vibrations Over Time (Sensor 4)"
    },
    subtitles: [{
      text: "Loading Data...",
      fontSize: 24,
      horizontalAlign: "center",
      verticalAlign: "center",
      dockInsidePlotArea: true
    }],
    axisX: {
      title: "Timestamp"
    },
    axisY: {
      title: "Vibrations [mm]"
    },
    data: [
      {
        type: "line",
        name: "Vibrations (sensor 4)",
        showInLegend: true,
        dataPoints: this.vibrationData
      }
    ]
  };

  constructor(
      private sensorService: SensorService
  ) {
  }

  public getVibrationChartInstance(chart: object) {
    this.vibrationChart = chart
    this.fetchVibrationData()
    this.vibrationChart.subtitles[0].remove();
  }

  private fetchVibrationData() {
    this.sensorService.getSensorsData(`{"SensorType":"vibrations", "SensorId":"9"}`).subscribe({
      next: sensors => {
        sensors.map(sensor => (this.vibrationData.push({x: new Date(sensor.timestamp), y: Number(sensor.value)})));
        this.vibrationChart.render()
      }
    });
  }
}
