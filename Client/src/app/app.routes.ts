import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { PatientLogsComponent } from './features/patient-logs/patient-logs.component';
import { PatientLogDetailsComponent } from './features/patient-log-details/patient-log-details.component';
import { PatientsComponent } from './features/patients/patients.component';
import { DentistsComponent } from './features/dentists/dentists.component';
import { PaymentsComponent } from './features/payments/payments.component';
import { ProceduresComponent } from './features/procedures/procedures.component';
import { PatientDetailsComponent } from './features/patient-details/patient-details.component';

export const routes: Routes = [
    { path: "", component: HomeComponent },
    { path: "patient-logs", component: PatientLogsComponent },
    { path: "patient-logs/:id", component: PatientLogDetailsComponent },
    { path: "patients", component: PatientsComponent },
    { path: "patients/:id", component: PatientDetailsComponent },
    { path: "dentists", component: DentistsComponent },
    { path: "payments", component: PaymentsComponent },
    { path: "procedures", component: ProceduresComponent },
    { path: "**", redirectTo: "", pathMatch: "full" }
];
