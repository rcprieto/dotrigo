import { Injectable, signal } from '@angular/core';

export interface Product {
  id: number;
  name: string;
  description: string;
  photo: string;
  size: string;
  weight: string;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private products = signal<Product[]>([]);

  constructor() {
    // Mock data
    this.products.set([
      {
        id: 1,
        name: 'Bolo de Chocolate',
        description: 'Delicioso bolo de chocolate com cobertura de brigadeiro.',
        photo: 'https://via.placeholder.com/250',
        size: 'Médio',
        weight: '1kg',
        price: 50.00
      },
      {
        id: 2,
        name: 'Torta de Limão',
        description: 'Torta de limão com merengue suíço.',
        photo: 'https://via.placeholder.com/250',
        size: 'Grande',
        weight: '1.5kg',
        price: 70.00
      }
    ]);
  }

  getProducts() {
    return this.products();
  }
}
