import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { environment } from "src/environments/environment";
import { HttpHelperService } from "../core/services/http-helper-service";
import { AmeGiftCardPaymentRequest, AmeGiftCardPaymentResponse, PagSeguroCreatePaymentRequest, PagSeguroCreatePaymentResponse } from "../models/payment-model";

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
            environment.paymentHubGatewayApi + "/payment/pix/qrcode/" + id,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }

    getAmeQrCode(id: string) {
           
        return this.http.get<string>(
            environment.paymentHubGatewayApi + "/payment/ame/qrcode/" + id,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }
    
    createPagSeguroPayment(request: PagSeguroCreatePaymentRequest) {
           
        return this.http.post<PagSeguroCreatePaymentResponse>(
            environment.paymentHubGatewayApi + "/payment/pagseguro", 
            request,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }

    createGiftCardPayment(request: AmeGiftCardPaymentRequest) {
           
        return this.http.post<AmeGiftCardPaymentResponse>(
            environment.paymentHubGatewayApi + "/payment/ame/giftcard", 
            request,
            this.httpHelperService.getHttpHeaderOptions()
        );
    }
}