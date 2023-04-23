import { TestBed } from '@angular/core/testing';

import { TavernService } from './tavern.service';

describe('TavernserviceService', () => {
  let service: TavernService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TavernService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
