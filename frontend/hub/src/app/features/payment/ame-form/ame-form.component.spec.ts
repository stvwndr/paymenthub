import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmeFormComponent } from './ame-form.component';

describe('AmeFormComponent', () => {
  let component: AmeFormComponent;
  let fixture: ComponentFixture<AmeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmeFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AmeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
