import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketrepliesComponent } from './ticketreplies-component';

describe('TicketrepliesComponent', () => {
  let component: TicketrepliesComponent;
  let fixture: ComponentFixture<TicketrepliesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketrepliesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketrepliesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
