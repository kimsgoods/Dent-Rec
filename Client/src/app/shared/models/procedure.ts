
export interface Procedure {
  id: number;
  name: string;
  description: string;
  pricingType: 'Fixed' | 'PerTooth';
  fee: number;
  createdOn: string;
  modifiedOn: string;
  createdBy: any;
  modifiedBy: any;
}

export class SelectedProcedure {
  procedure: Procedure;
  quantity: number;
  notes?: string;

  constructor(procedure: Procedure, quantity: number = 1, notes?: string) {
    this.procedure = procedure;
    this.quantity = quantity;
    this.notes = notes;
  }
}

export interface GetPatientLogProcedure {
  procedure: Procedure
  quantity: number
  notes: string
  adjustedFee: number
}
