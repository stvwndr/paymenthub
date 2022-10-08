import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { HttpHelperService } from "../core/services/http-helper-service";
import { AmeGiftCardPaymentRequest, PagSeguroCreatePaymentRequest, PagSeguroCreatePaymentResponse } from "../models/payment-model";

@Injectable({
    providedIn: 'root' 
})
export class PaymentHubService{

    constructor(
        private http: HttpClient,
        private httpHelperService: HttpHelperService
    ) {}

    getPixQrCode(id: string) {
           
        return this.http.get<string>(
            "http://localhost:7080/payment/pix/qrcode/" + id,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }

    getAmeQrCode(id: string) {
           
        return this.http.get<string>(
            "http://localhost:7080/payment/ame/qrcode/" + id,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }
    
    createPagSeguroPayment(request: PagSeguroCreatePaymentRequest) {
           
        return this.http.post<PagSeguroCreatePaymentResponse>(
            "http://localhost:7080/payment/pagseguro", 
            request,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }

    createGiftCardPayment(request: AmeGiftCardPaymentRequest) {
           
        return this.http.post<AmeGiftCardPaymentRequest>(
            "http://localhost:7080/payment/ame/giftcard", 
            request,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }
}