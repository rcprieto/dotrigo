import { Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list';
import { CartComponent } from './cart/cart';

export const routes: Routes = [
  { path: '', component: ProductListComponent },
  { path: 'cart', component: CartComponent },
];
