import { TestBed } from '@angular/core/testing';

import { ReadyProducts } from './ready-products';

describe('ReadyProducts', () => {
  let service: ReadyProducts;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReadyProducts);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
