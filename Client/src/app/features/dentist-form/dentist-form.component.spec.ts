import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DentistFormComponent } from './dentist-form.component';

describe('DentistFormComponent', () => {
  let component: DentistFormComponent;
  let fixture: ComponentFixture<DentistFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DentistFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DentistFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
