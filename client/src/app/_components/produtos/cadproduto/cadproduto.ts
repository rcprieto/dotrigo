import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { Enumerador, Produto } from '../../../_models/entidades';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProdutoService } from '../../../_services/produto.service';
import { EnumeradorService } from '../../../_services/enumerador.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

type Pagina = 'produtos' | 'pedidos';

@Component({
  selector: 'app-cadproduto',
  imports: [CommonModule, FormsModule],
  templateUrl: './cadproduto.html',
  styleUrl: './cadproduto.css',
})
export class Cadproduto implements OnInit {
  service = inject(ProdutoService);
  enumeradorService = inject(EnumeradorService);
  categorias = signal<Enumerador[]>([]);
  toast = inject(ToastrService);
  produtos = signal<Produto[]>([]);
  produtosTodos = signal<Produto[]>([]);

  paginaAtual: WritableSignal<Pagina> = signal('produtos');
  categoriaAtiva = signal('produtos');
  mostrarCadastro = signal(false);
  itemCadastro = signal<Produto>({
    id: 0,
    nome: '',
    categoriaId: null,
    categoria: null,
    fotoUrl: null,
    descricao: null,
    tamanho: null,
    peso: null,
    preco: 0,
    tempoPreparo: null,
    prontaEntrega: false,
    ativo: true,
    dataCadastro: '',
    pedidoItens: [],
    estoque: 0,
  });

  ngOnInit(): void {
    this.enumeradorService
      .listar()
      .pipe(take(1))
      .subscribe({
        next: (resp) => {
          this.categorias.set(resp.filter((c) => c.referenciaId == 2 && c.id != 9));
        },
      });
    this.carregarProdutos();
  }
  filtrar(event: any) {
    this.produtos.set(
      this.produtosTodos().filter((c) =>
        c.nome.toUpperCase().includes(event.target.value.toUpperCase())
      )
    );
  }
  carregarProdutos() {
    this.service
      .listarProdutos()
      .pipe(take(1))
      .subscribe({
        next: (resp) => {
          this.produtos.set(resp);
          this.produtosTodos.set(resp);
        },
      });
  }
  navegarPara(pagina: string) {
    this.categoriaAtiva.set(pagina);
  }
  editar(item: Produto) {
    this.itemCadastro.set(item);
    this.mostrarCadastro.set(true);
  }
  salvar() {
    this.itemCadastro().dataCadastro = '';
    if (this.itemCadastro().id > 0) {
      this.service
        .atualizar(this.itemCadastro())
        .pipe(take(1))
        .subscribe({
          next: (resp) => {
            //this.itemCadastro.set(resp);
            this.toast.success('Cadastro Realizado com Sucesso');
            this.cancelar();
            this.carregarProdutos();
          },
        });
    } else {
      this.service
        .cadastrar(this.itemCadastro())
        .pipe(take(1))
        .subscribe({
          next: (resp) => {
            //this.itemCadastro.set(resp);
            this.toast.success('Cadastro Realizado com Sucesso');
            this.cancelar();
            this.carregarProdutos();
          },
        });
    }
  }
  cancelar() {
    this.mostrarCadastro.set(false);
    this.itemCadastro.set({
      id: 0,
      nome: '',
      categoriaId: null,
      categoria: null,
      fotoUrl: null,
      descricao: null,
      tamanho: null,
      peso: null,
      preco: 0,
      tempoPreparo: null,
      prontaEntrega: false,
      ativo: false,
      dataCadastro: '',
      pedidoItens: [],
      estoque: null,
    });
  }
}
