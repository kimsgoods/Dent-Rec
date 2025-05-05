import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogFiltersComponent } from './patient-log-filters.component';

describe('PatientLogFiltersComponent', () => {
  let component: PatientLogFiltersComponent;
  let fixture: ComponentFixture<PatientLogFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogFiltersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
