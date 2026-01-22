import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SLAComponent } from './sla-component';

describe('SLAComponent', () => {
  let component: SLAComponent;
  let fixture: ComponentFixture<SLAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SLAComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SLAComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
