import { Injectable } from '@angular/core';
import { of } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  price: number;
  image: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private products: Product[] = [
    { id: 1, name: 'Croissant', price: 2.50, image: '' },
    { id: 2, name: 'Baguette', price: 3.00, image: '' },
    { id: 3, name: 'Macaron', price: 1.50, image: '' },
    { id: 4, name: 'Éclair', price: 3.50, image: '' },
  ];

  getProducts() {
    return of(this.products);
  }
}
