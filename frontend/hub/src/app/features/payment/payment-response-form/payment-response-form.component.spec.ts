import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentResponseFormComponent } from './payment-response-form.component';

describe('PaymentResponseFormComponent', () => {
  let component: PaymentResponseFormComponent;
  let fixture: ComponentFixture<PaymentResponseFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentResponseFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentResponseFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
