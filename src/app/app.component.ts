import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
    <header>
      <h1>French Bakery</h1>
      <nav>
        <a routerLink="/">Products</a>
        <a routerLink="/cart">Cart</a>
      </nav>
    </header>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
  styles: [`
    header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 1rem;
      background-color: #333;
      color: white;
    }

    nav a {
      color: white;
      text-decoration: none;
      margin-left: 1rem;
    }

    main {
      padding: 1rem;
    }
  `],
  imports: [RouterOutlet, RouterLink],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {}
