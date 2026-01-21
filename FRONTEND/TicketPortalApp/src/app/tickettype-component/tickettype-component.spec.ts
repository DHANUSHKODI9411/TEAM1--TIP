import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketTypeComponent } from './tickettype-component';

describe('TickettypeComponent', () => {
  let component: TicketTypeComponent;
  let fixture: ComponentFixture<TicketTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketTypeComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
