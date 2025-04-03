import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogDetailsComponent } from './patient-log-details.component';

describe('PatientLogDetailsComponent', () => {
  let component: PatientLogDetailsComponent;
  let fixture: ComponentFixture<PatientLogDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
