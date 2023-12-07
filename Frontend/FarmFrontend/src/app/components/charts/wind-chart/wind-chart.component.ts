import {Component, OnDestroy, OnInit} from '@angular/core';
import {SensorService} from "../../../service/sensor.service";

@Component({
    selector: 'wind-chart',
    templateUrl: './wind-chart.component.html',
    styleUrl: './wind-chart.component.css'
})
export class WindChartComponent {
    public windData: { x: Date, y: number }[] = [];

    public windChart: any

    public windChartOptions = {
        title: {
            text: "Wind Speed Over Time (Sensor 4)"
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
            title: "Wind speed [m/s]"
        },
        data: [
            {
                type: "line",
                name: "Wind Speed (sensor 4)",
                showInLegend: true,
                dataPoints: this.windData
            }
        ]
    };

    constructor(
        private sensorService: SensorService
    ) {
    }

    public getWindChartInstance(chart: object) {
        this.windChart = chart
        this.fetchWindData()
        this.windChart.subtitles[0].remove();
    }

    private fetchWindData() {
        this.sensorService.getSensorsData(`{"SensorType":"wind_speed", "SensorId":"4"}`).subscribe({
            next: sensors => {
                sensors.map(sensor => (this.windData.push({x: new Date(sensor.timestamp), y: Number(sensor.value)})));
                this.windChart.render()
            }
        });
    }
}
