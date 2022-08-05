import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SerarchByPnrEmailComponent } from './serarch-by-pnr-email.component';

describe('SerarchByPnrEmailComponent', () => {
  let component: SerarchByPnrEmailComponent;
  let fixture: ComponentFixture<SerarchByPnrEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SerarchByPnrEmailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SerarchByPnrEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
