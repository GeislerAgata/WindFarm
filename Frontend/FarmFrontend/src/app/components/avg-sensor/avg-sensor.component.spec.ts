import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvgSensorComponent } from './avg-sensor.component';

describe('AvgSensorComponent', () => {
  let component: AvgSensorComponent;
  let fixture: ComponentFixture<AvgSensorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AvgSensorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AvgSensorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
