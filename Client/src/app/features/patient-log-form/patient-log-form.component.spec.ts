import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientLogFormComponent } from './patient-log-form.component';

describe('PatientLogFormComponent', () => {
  let component: PatientLogFormComponent;
  let fixture: ComponentFixture<PatientLogFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientLogFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientLogFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
