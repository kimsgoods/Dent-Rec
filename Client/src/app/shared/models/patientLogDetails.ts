import { Procedure } from "./procedure";

export interface PatientLogDetails {
    id: number;
    patientName: string;
    dentistName: string;
    procedures: Procedure[];
    payments: any[];
    gender: string;
    age: number;
    address: string;
    procedureDate: string;
    notes: string;
    fee: number;
    patientId: number;
    dentistId: number;
    createdOn: string;
    modifiedOn: string;
    createdBy: any;
    modifiedBy: any;
}
