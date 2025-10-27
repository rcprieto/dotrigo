import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ReadyProductsService, ReadyProduct } from '../ready-products';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-ready-products',
  templateUrl: './ready-products.html',
  styleUrl: './ready-products.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CurrencyPipe]
})
export class ReadyProductsComponent {
  private readyProductsService = inject(ReadyProductsService);
  products: ReadyProduct[] = [];

  ngOnInit() {
    this.products = this.readyProductsService.getProducts();
  }
}
