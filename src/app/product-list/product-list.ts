import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { Product, ProductService } from '../product';
import { CartService } from '../cart';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
  imports: [AsyncPipe, CurrencyPipe],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductListComponent {
  private productService = inject(ProductService);
  private cartService = inject(CartService);

  products$ = this.productService.getProducts();

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }
}
