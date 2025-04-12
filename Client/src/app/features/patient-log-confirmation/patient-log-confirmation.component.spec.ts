import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogConfirmationComponent } from './patient-log-confirmation.component';

describe('PatientLogConfirmationComponent', () => {
  let component: PatientLogConfirmationComponent;
  let fixture: ComponentFixture<PatientLogConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogConfirmationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
