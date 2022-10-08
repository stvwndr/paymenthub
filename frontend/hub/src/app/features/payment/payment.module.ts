import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';

import { PaymentFormComponent } from './payment-form/payment-form.component';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { GiftCardFormComponent } from './gift-card-form/gift-card-form.component';
import { PagseguroFormComponent } from './pagseguro-form/pagseguro-form.component';
import { PixFormComponent } from './pix-form/pix-form.component';
import { AmeFormComponent } from './ame-form/ame-form.component';

@NgModule({
  declarations: [
    PaymentFormComponent,
    GiftCardFormComponent,
    PagseguroFormComponent,
    PixFormComponent,
    AmeFormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatGridListModule,
    MatIconModule,
  ],
  exports: [
    PaymentFormComponent
  ]
})
export class PaymentModule { }
