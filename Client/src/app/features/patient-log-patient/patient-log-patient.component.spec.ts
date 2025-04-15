import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogPatientComponent } from './patient-log-patient.component';

describe('PatientSearchComponent', () => {
  let component: PatientLogPatientComponent;
  let fixture: ComponentFixture<PatientLogPatientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogPatientComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogPatientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
