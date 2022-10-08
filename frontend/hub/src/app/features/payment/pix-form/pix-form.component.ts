import { Component, OnInit } from '@angular/core';

import { v4 as uuid } from 'uuid';
import { PaymentHubService } from 'src/app/services/payment-hub.service';

@Component({
  selector: 'app-pix-form',
  templateUrl: './pix-form.component.html',
  styleUrls: ['./pix-form.component.css']
})
export class PixFormComponent implements OnInit {

  qrCode: string = "";

  constructor(
    private paymentHubService: PaymentHubService
  ) { }

  ngOnInit(): void {
    
    this.getPixQrCode();
  }

  getPixQrCode(): void {

    var customerId = uuid();

    this.paymentHubService.getPixQrCode(customerId)
      .subscribe(
        data => {
          this.qrCode = data;
          console.log('Pix QrCode obtido com sucesso: ' + data)
        },
        error => console.log('--->>>>>' + error.error.details[0].message)
      )
  }

}
function uuidv4() {
  throw new Error('Function not implemented.');
}

