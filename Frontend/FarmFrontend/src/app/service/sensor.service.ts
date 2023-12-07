import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable} from "rxjs";
import {Sensor} from "../app-modules";
import {HttpClient} from "@angular/common/http";
import {SensorStats} from "../types/sensor-types";

@Injectable({
  providedIn: 'root',
})
export class SensorService {
  private dataSubject = new BehaviorSubject<Sensor[]>([]);
  data$ = this.dataSubject.asObservable();

  constructor(private http: HttpClient) {
  }

  public getSensorAverageData(sensorId: number): Observable<SensorStats> {
    return this.http.get<SensorStats>('https://localhost:7082/api/SensorData/' + sensorId + '/avg')
  }

  public getSensorsData(filters: string = "{}", sortBy: string = "", order: string = ""): Observable<Sensor[]> {
    const url = `https://localhost:7082/api/SensorData/?filters=${filters}&sortBy=desc&order=${order}&limit=10`
    return this.http.get<Sensor[]>(url)
  }
}
