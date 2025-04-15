import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogPaymentComponent } from './patient-log-payment.component';

describe('PaymentFormComponent', () => {
  let component: PatientLogPaymentComponent;
  let fixture: ComponentFixture<PatientLogPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogPaymentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
