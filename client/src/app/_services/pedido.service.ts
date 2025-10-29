import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../_environment/environment.development';
import { Pedido } from '../_models/entidades';

@Injectable({
  providedIn: 'root',
})
export class PedidoService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl + '/pedido';

  criarPedido(pedidoDto: Pedido) {
    return this.http.post<Pedido>(this.baseUrl, pedidoDto);
  }

  buscarPedidoPorId(id: number) {
    return this.http.get<Pedido>(`${this.baseUrl}/${id}`);
  }
}
