import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { ProductListComponent } from './app/product-list/product-list';
import { CartComponent } from './app/cart/cart';
import { ReadyProductsComponent } from './app/ready-products/ready-products';
import { AdminComponent } from './app/admin/admin';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter([
      { path: '', component: ProductListComponent },
      { path: 'cart', component: CartComponent },
      { path: 'ready-products', component: ReadyProductsComponent },
      { path: 'admin', component: AdminComponent },
    ]),
  ],
});
