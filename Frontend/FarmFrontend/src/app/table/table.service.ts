import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Sensor } from "../app-modules";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class TableService {
    private baseUrl = 'https://localhost:7082/api/SensorData';

    private dataSubject = new BehaviorSubject<Sensor[]>([]);
    data$ = this.dataSubject.asObservable();

    constructor(private http: HttpClient) {}

    getSensors(filters: string = "{}", sortBy: string = "", order: string = ""): void {
        const url = `${this.baseUrl}?filters=${filters}&sortBy=${sortBy}&order=${order}`
        this.http.get<Sensor[]>(url)
        .subscribe(val => this.dataSubject.next(val));
    }
}