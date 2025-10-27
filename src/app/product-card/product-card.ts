import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { Product } from '../product.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.html',
  styleUrl: './product-card.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductCardComponent {
  product = input.required<Product>();
}
