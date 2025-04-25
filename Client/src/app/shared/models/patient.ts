import { PatientLog } from "./patientLog"
import { Payment } from "./payment"

export interface Patient {
    id: number
    firstName: string
    lastName: string
    gender: string
    age: number
    phone: string
    email: string
    address: string
  }

  export interface PatientDetails {
    id: number
    firstName: string
    lastName: string
    gender: string
    age: number
    phone: string
    email: string
    address: string
    payments: Payment[]
    patientLogs: PatientLog[]
    createdOn: string
    modifiedOn: string
    createdBy: string
    modifiedBy: string
  }