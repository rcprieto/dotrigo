import { Routes } from '@angular/router';
import { Produtos } from './_components/produtos/produtos';
import { Cadproduto } from './_components/produtos/cadproduto/cadproduto';
import { Listapedidos } from './_components/pedidos/listapedidos/listapedidos';

export const routes: Routes = [
  { path: '', component: Produtos },
  { path: 'adminprodutos', component: Cadproduto },
  { path: 'adminpedidos', component: Listapedidos },
];
