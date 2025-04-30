import { Payment } from "./payment";
import { GetPatientLogProcedure } from "./procedure";

export interface PatientLogDetails {
    id: number
  patientId: number
  patientName: string
  gender: string
  age: number
  address: string
  dentistId: number
  dentistName: string
  procedureDate: string
  notes: string
  fee: number
  paymentStatus: string
  procedures: GetPatientLogProcedure[]
  payments: Payment[]
  createdOn: string
  modifiedOn: string
  createdBy: any
  modifiedBy: any
}


