import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleAirlineComponent } from './schedule-airline.component';

describe('ScheduleAirlineComponent', () => {
  let component: ScheduleAirlineComponent;
  let fixture: ComponentFixture<ScheduleAirlineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScheduleAirlineComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleAirlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
