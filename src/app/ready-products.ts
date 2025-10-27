import { Injectable } from '@angular/core';
import { signal } from '@angular/core';

export interface ReadyProduct {
  id: number;
  name: string;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class ReadyProductsService {
  private products = signal<ReadyProduct[]>([
    { id: 1, name: 'Ready Product 1', price: 100 },
    { id: 2, name: 'Ready Product 2', price: 200 },
    { id: 3, name: 'Ready Product 3', price: 300 },
  ]);

  getProducts() {
    return this.products();
  }
}
