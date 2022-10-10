import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PaymentHubService } from 'src/app/services/payment-hub.service';
import { AmeGiftCardPaymentRequest, PaymentHubResponse } from 'src/app/models/payment-model';

@Component({
  selector: 'app-gift-card-form',
  templateUrl: './gift-card-form.component.html',
  styleUrls: ['./gift-card-form.component.css']
})
export class GiftCardFormComponent implements OnInit {

  paymentForm!: FormGroup;
  paymentResponse!: PaymentHubResponse;
  submited: boolean = false;

  constructor(    
    private paymentHubService: PaymentHubService,
    private formBuilder: FormBuilder) 
    { }

  ngOnInit(): void {
    
    this.paymentForm = this.formBuilder.group({
        giftCardNumber: ['', [Validators.required]],
        amount: ['', [Validators.required]],
      });
    }
    
  onSubmit(): void {

    this.submited = true;

    if (this.paymentForm.invalid) { return; }

    const giftCard = this.mapToGiftCard();

    if (giftCard != null)
    {
      this.paymentHubService.createGiftCardPayment(giftCard)
        .subscribe(
          data => {
            this.paymentResponse = {
              paymentId: data.paymentId,
              status: data.status,
              amount: data.amount
            };
            console.log('Pagamento com cartão-presente realizado com sucesso: ' + data);            
        },
          error => console.log('--->>>>>' + error.error.details[0].message)
        )
    }
  }

  mapToGiftCard(): AmeGiftCardPaymentRequest {

    return {
      giftCardNumber: this.paymentForm.get('giftCardId')?.value,
      amount: this.paymentForm.get('amount')?.value,
    } as AmeGiftCardPaymentRequest;
  }

  isInvalid(controlName: string): boolean {

    return this.submited && this.paymentForm.get(controlName)?.errors != null;
  }
}
