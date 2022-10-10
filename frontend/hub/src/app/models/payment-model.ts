import { PaymentStatus } from "./payment-enums";

export interface PagSeguroCreatePaymentRequest {
    givenName: string;    
    validThru: string;
    cardNumber: string;
    code: number;
    amount: number;
}

export interface PagSeguroCreatePaymentResponse {
    paymentId: string;
    amount: number;
    status: PaymentStatus;
}

export interface AmeGiftCardPaymentRequest {
    giftCardNumber: string;
    amount: number;
}

export interface AmeGiftCardPaymentResponse {
    paymentId: string;
    amount: number;
    status: PaymentStatus;
}

export interface PaymentHubResponse {
    paymentId: string;
    amount: number;
    status: PaymentStatus
}