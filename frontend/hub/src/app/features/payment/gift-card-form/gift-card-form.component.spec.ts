import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftCardFormComponent } from './gift-card-form.component';

describe('GiftCardFormComponent', () => {
  let component: GiftCardFormComponent;
  let fixture: ComponentFixture<GiftCardFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GiftCardFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GiftCardFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
