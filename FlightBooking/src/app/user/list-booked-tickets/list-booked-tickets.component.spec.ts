import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBookedTicketsComponent } from './list-booked-tickets.component';

describe('ListBookedTicketsComponent', () => {
  let component: ListBookedTicketsComponent;
  let fixture: ComponentFixture<ListBookedTicketsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListBookedTicketsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBookedTicketsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
