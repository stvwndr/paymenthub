import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagseguroFormComponent } from './pagseguro-form.component';

describe('PagseguroFormComponent', () => {
  let component: PagseguroFormComponent;
  let fixture: ComponentFixture<PagseguroFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagseguroFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagseguroFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
