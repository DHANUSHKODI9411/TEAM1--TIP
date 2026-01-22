import { TestBed } from '@angular/core/testing';

import { TicketrepliesServices } from './ticketreplies-services';

describe('TicketrepliesServices', () => {
  let service: TicketrepliesServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicketrepliesServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
