import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogSummaryComponent } from './patient-log-summary.component';

describe('PatientLogSummaryComponent', () => {
  let component: PatientLogSummaryComponent;
  let fixture: ComponentFixture<PatientLogSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogSummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
