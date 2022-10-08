import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagSeguroCreatePaymentRequest } from 'src/app/models/payment-model';
import { PaymentHubService } from 'src/app/services/payment-hub.service';

@Component({
  selector: 'app-payment-form',
  templateUrl: './payment-form.component.html',
  styleUrls: ['./payment-form.component.css']
})
export class PaymentFormComponent implements OnInit {

  paymentForm!: FormGroup;
  submited: boolean = false;
  
  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    
    this.paymentForm = this.formBuilder.group({
      customerId: ['', [Validators.required]],
    });
  }

  

  mapToPixQrCode(): string {

    return this.paymentForm.get('customerId')?.value;
  }

  mapToPagSeguroCreatePaymentRequest(): PagSeguroCreatePaymentRequest {

    return {
      givenName: this.paymentForm.get('givenName')?.value,
      validThru: this.paymentForm.get('validThru')?.value,
      cardNumber: this.paymentForm.get('cardNumber')?.value,
      code: this.paymentForm.get('code')?.value,
      amount: this.paymentForm.get('amount')?.value
    } as PagSeguroCreatePaymentRequest;
  }

  isInvalid(controlName: string): boolean {

    return this.submited && this.paymentForm.get(controlName)?.errors != null;
  }
}
