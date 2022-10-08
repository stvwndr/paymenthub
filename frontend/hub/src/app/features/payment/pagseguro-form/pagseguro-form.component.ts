import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagSeguroCreatePaymentRequest } from 'src/app/models/payment-model';
import { PaymentHubService } from 'src/app/services/payment-hub.service';

@Component({
  selector: 'app-pagseguro-form',
  templateUrl: './pagseguro-form.component.html',
  styleUrls: ['./pagseguro-form.component.css']
})
export class PagseguroFormComponent implements OnInit {

  paymentForm!: FormGroup;
  submited: boolean = false;

  constructor(    
    private paymentHubService: PaymentHubService,
    private formBuilder: FormBuilder) 
    { }

  ngOnInit(): void {
    
    this.paymentForm = this.formBuilder.group({
        givenName: ['', [Validators.required]],
        cardNumber: ['', [Validators.required]],
        validThru: ['', [Validators.required]],
        code: ['', [Validators.required]],
        amount: ['', [Validators.required]],        
      });
    }
    
  onSubmit(): void {

    this.submited = true;

    if (this.paymentForm.invalid) { return; }

    const pagSeguroPayment = this.mapToPagSeguroCreatePaymentRequest();

    if (pagSeguroPayment != null)
    {
      this.paymentHubService.createPagSeguroPayment(pagSeguroPayment)
        .subscribe(
          data => console.log('Pagamento com cartão realizado com sucesso: ' + data),
          error => console.log('--->>>>>' + error.error.details[0].message)
        )
    }
  }

  mapToPagSeguroCreatePaymentRequest(): PagSeguroCreatePaymentRequest {

    return {
      givenName: this.paymentForm.get('givenName')?.value,
      cardNumber: this.paymentForm.get('cardNumber')?.value,
      validThru: this.paymentForm.get('validThru')?.value,
      code: this.paymentForm.get('code')?.value,
      amount: this.paymentForm.get('amount')?.value
    } as PagSeguroCreatePaymentRequest;
  }

  isInvalid(controlName: string): boolean {

    return this.submited && this.paymentForm.get(controlName)?.errors != null;
  }

}
