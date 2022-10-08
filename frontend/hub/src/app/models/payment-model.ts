export interface PagSeguroCreatePaymentRequest {
    givenName: string;    
    validThru: string;
    cardNumber: number;
    code: number;
    amount: number;
}

export interface PagSeguroCreatePaymentResponse {
    paymentId: string;
    status: string;
    amount: number;
}

export interface AmeGiftCardPaymentRequest {
    giftCardNumber: string;
    amount: number;
}
