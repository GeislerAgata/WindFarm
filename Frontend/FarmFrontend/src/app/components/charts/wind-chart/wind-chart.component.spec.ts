import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WindChartComponent } from './wind-chart.component';

describe('ChartsComponent', () => {
  let component: WindChartComponent;
  let fixture: ComponentFixture<WindChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WindChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WindChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
