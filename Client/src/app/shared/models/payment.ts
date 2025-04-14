export interface NewPayment {
    patientLogId: number;
    patientId: number;
    paymentMethod: string;
    amount: number;
}

export interface Payment {
    id: number;
    patientLogId: number;
    patientId: number;
    paymentMethod: string;
    amount: number;
}

