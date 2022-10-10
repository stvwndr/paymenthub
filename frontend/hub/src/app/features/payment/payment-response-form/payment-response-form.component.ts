import { Component, Input, OnInit } from '@angular/core';

import { PaymentHubResponse } from 'src/app/models/payment-model';

@Component({
  selector: 'app-payment-response-form',
  templateUrl: './payment-response-form.component.html',
  styleUrls: ['./payment-response-form.component.css']
})
export class PaymentResponseFormComponent implements OnInit {

  @Input() paymentResponse!: PaymentHubResponse;

  constructor() { }

  ngOnInit(): void {
  }
}
