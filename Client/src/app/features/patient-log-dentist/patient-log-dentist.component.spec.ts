import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogDentistComponent } from './patient-log-dentist.component';

describe('PatientLogDentistComponent', () => {
  let component: PatientLogDentistComponent;
  let fixture: ComponentFixture<PatientLogDentistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogDentistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogDentistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
