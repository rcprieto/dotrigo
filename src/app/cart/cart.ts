import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../cart';
import { Product } from '../product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.html',
  styleUrl: './cart.css',
  imports: [CurrencyPipe],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CartComponent {
  private cartService = inject(CartService);

  cart = this.cartService.cart;

  get total() {
    return this.cart().reduce((sum, product) => sum + product.price, 0);
  }

  clearCart() {
    this.cartService.clearCart();
  }
}
