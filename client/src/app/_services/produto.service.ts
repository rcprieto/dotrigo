import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../_environment/environment.development';
import { Produto } from '../_models/entidades';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {
  // Injeção moderna do HttpClient
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl + '/produto';

  listarProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseUrl);
  }

  /**
   * Busca a lista de produtos de pronta entrega.
   * Corresponde a: GET /api/produtos/pronta-entrega
   */
  listarProntaEntrega(): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${this.baseUrl}/pronta-entrega`);
  }

  /**
   * Busca um produto específico pelo seu ID.
   * Corresponde a: GET /api/produtos/{id}
   */
  buscarProdutoPorId(id: string): Observable<Produto> {
    return this.http.get<Produto>(`${this.baseUrl}/${id}`);
  }

  cadastrar(model: Produto) {
    return this.http.post<Produto>(this.baseUrl, model);
  }
  atualizar(model: Produto) {
    return this.http.put<Produto>(this.baseUrl, model);
  }
}
