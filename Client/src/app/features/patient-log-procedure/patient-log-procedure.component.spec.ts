import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogProcedureComponent } from './patient-log-procedure.component';

describe('PatientLogProcedureComponent', () => {
  let component: PatientLogProcedureComponent;
  let fixture: ComponentFixture<PatientLogProcedureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogProcedureComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
