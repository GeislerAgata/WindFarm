import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VibrationsChartComponent } from './vibrations-chart.component';

describe('VibrationsChartComponent', () => {
  let component: VibrationsChartComponent;
  let fixture: ComponentFixture<VibrationsChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VibrationsChartComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VibrationsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
