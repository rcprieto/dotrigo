import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { environment } from '../../_environment/environment';
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

  inserirAnexo(arquivo: File[], id: number | null = null) {
    const formData: FormData = new FormData();
    arquivo.forEach((file) => {
      formData.append('arquivos', file, file.name);
    });
    if (id) {
      formData.append('id', id.toString());
    }

    return this.http
      .post(`${this.baseUrl}/salvar-foto`, formData)
      .pipe(take(1))
      .subscribe({
        next: (item) => {},
        error: (error) => {
          console.log(error);
        },
      });
  }
}
