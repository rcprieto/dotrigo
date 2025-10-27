import { Injectable, signal } from '@angular/core';
import { Product } from './product';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  cart = signal<Product[]>([]);

  addToCart(product: Product) {
    this.cart.update(items => [...items, product]);
  }

  getCart() {
    return this.cart();
  }

  clearCart() {
    this.cart.set([]);
  }
}
