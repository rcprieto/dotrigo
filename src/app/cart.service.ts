import { Injectable, signal } from '@angular/core';
import { Product } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cart = signal<Product[]>([]);

  addToCart(product: Product) {
    this.cart.update(currentCart => [...currentCart, product]);
  }

  getCart() {
    return this.cart();
  }

  clearCart() {
    this.cart.set([]);
  }
}
