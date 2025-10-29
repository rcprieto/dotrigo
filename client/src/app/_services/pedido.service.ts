import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../_environment/environment.development';
import { CriarPedidoDto } from '../_models/dtos';
import { Pedido } from '../_models/entidades';

@Injectable({
  providedIn: 'root',
})
export class PedidoService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  /**
   * Envia um novo pedido (checkout) para a API.
   * Corresponde a: POST /api/pedidos
   * @param pedidoDto O DTO com os dados do cliente e os itens do carrinho.
   */
  criarPedido(pedidoDto: CriarPedidoDto): Observable<Pedido> {
    return this.http.post<Pedido>(this.baseUrl, pedidoDto);
  }

  /**
   * Busca um pedido específico pelo ID (ex: para uma tela de "Obrigado").
   * Corresponde a: GET /api/pedidos/{id}
   */
  buscarPedidoPorId(id: number): Observable<Pedido> {
    return this.http.get<Pedido>(`${this.baseUrl}/${id}`);
  }
}
