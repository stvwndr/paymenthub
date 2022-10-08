import { Component, OnInit } from '@angular/core';

import { v4 as uuid } from 'uuid';
import { PaymentHubService } from 'src/app/services/payment-hub.service';

@Component({
  selector: 'app-ame-form',
  templateUrl: './ame-form.component.html',
  styleUrls: ['./ame-form.component.css']
})
export class AmeFormComponent implements OnInit {

  qrCode: string = "";

  constructor(
    private paymentHubService: PaymentHubService
  ) { }

  ngOnInit(): void {
    
    this.getAmeQrCode();
  }

  getAmeQrCode(): void {

    var customerId = uuid();

    this.paymentHubService.getAmeQrCode(customerId)
      .subscribe(
        data => {
          this.qrCode = data;
          console.log('Ame QrCode obtido com sucesso: ' + data)
        },
        error => console.log('--->>>>>' + error.error.details[0].message)
      )
  }

}
