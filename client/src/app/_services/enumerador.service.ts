import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../_environment/environment';
import { Enumerador, Produto } from '../_models/entidades';

@Injectable({
  providedIn: 'root',
})
export class EnumeradorService {
  // Injeção moderna do HttpClient
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl + '/enumerador';

  listar(): Observable<Enumerador[]> {
    return this.http.get<Enumerador[]>(this.baseUrl);
  }
}
