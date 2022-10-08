import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PaymentFormEnum } from 'src/app/models/payment-enums';

@Component({
  selector: 'app-payment-form',
  templateUrl: './payment-form.component.html',
  styleUrls: ['./payment-form.component.css']
})
export class PaymentFormComponent implements OnInit {

  paymentForm!: FormGroup;
  submited: boolean = false;
  currentPaymentForm!: PaymentFormEnum;
  paymentFormEnum!: typeof PaymentFormEnum;

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    
    this.currentPaymentForm = PaymentFormEnum.CreditCard;
    this.paymentFormEnum = PaymentFormEnum;
    this.paymentForm = this.formBuilder.group({
      customerId: ['', [Validators.required]],
    });
  }

  changePaymentForm(event: Event): void {
        
    var target = (event.currentTarget as Element);

    switch (target.id) {
      case "btnPix":
        this.currentPaymentForm = PaymentFormEnum.Pix;
        break;
      case "btnGiftCard":
        this.currentPaymentForm = PaymentFormEnum.GiftCard;
        break;
      case "btnAme":
        this.currentPaymentForm = PaymentFormEnum.Ame;
        break;
      default:
        this.currentPaymentForm = PaymentFormEnum.CreditCard;
        break;
    }
  }

}
