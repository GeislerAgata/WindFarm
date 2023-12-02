import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  sensors: any[] = [
    { id: 1, temperature: 25, humidity: 60 },
    { id: 2, temperature: 22, humidity: 55 },
    // Add more sensor data
  ];

  constructor() {}

  ngOnInit(): void {}
}
