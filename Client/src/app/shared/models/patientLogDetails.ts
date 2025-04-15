import { Payment } from "./payment";
import { Procedure } from "./procedure";

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
  procedures: Procedure[]
  payments: Payment[]
  createdOn: string
  modifiedOn: string
  createdBy: any
  modifiedBy: any
}


