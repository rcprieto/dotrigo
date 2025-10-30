import {
  Component,
  ChangeDetectionStrategy,
  signal,
  computed,
  WritableSignal,
  effect,
  OnInit,
  inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProdutoService } from '../../_services/produto.service';
import { Enumerador, Pedido, PedidoItem, Produto } from '../../_models/entidades';
import { EnumeradorService } from '../../_services/enumerador.service';
import { take } from 'rxjs';
import { PedidoService } from '../../_services/pedido.service';
import { ToastrService } from 'ngx-toastr';
import { QrCodePix } from 'qrcode-pix';

type Pagina = 'menu' | 'carrinho' | 'checkout' | 'prontaEntrega' | 'confirmacao';
type StatusPedido = 'idle' | 'enviando' | 'sucesso' | 'erro';

// --- Componente Principal (Standalone) ---
@Component({
  selector: 'app-produtos',
  standalone: true,
  imports: [CommonModule, FormsModule], // Importa módulos necessários
  changeDetection: ChangeDetectionStrategy.OnPush, // Estratégia de detecção de mudanças
  templateUrl: './produtos.html',
  styleUrl: './produtos.css',
})
export class Produtos implements OnInit {
  service = inject(ProdutoService);
  enumeradorService = inject(EnumeradorService);
  pedidoService = inject(PedidoService);
  toasterService = inject(ToastrService);

  // Dados dos Produtos
  produtos = signal<Produto[]>([]);
  paginaAtual: WritableSignal<Pagina> = signal('menu');

  // Filtro de Categoria
  categoriaAtiva: WritableSignal<Enumerador['id']> = signal(9);
  categorias = signal<Enumerador[]>([]);

  // Carrinho de Compras
  carrinho: WritableSignal<PedidoItem[]> = signal(this.carregarCarrinho());

  // Dados do Formulário de Checkout
  clienteNome: WritableSignal<string> = signal('');
  clienteTelefone: WritableSignal<string> = signal('');
  clienteComentarios: WritableSignal<string> = signal('');
  clienteEndereco: WritableSignal<string> = signal('');
  clienteEmail: WritableSignal<string> = signal('');

  // Status do Envio do Pedido
  statusPedido: WritableSignal<StatusPedido> = signal('idle');
  ultimoPedido: WritableSignal<{ id: string; clienteNome: string }> = signal({
    id: '',
    clienteNome: '',
  });

  qrCodePix = QrCodePix({
    version: '01',
    key: '0e1e80a5-f2b0-4709-b123-7bf87d2f714d',
    name: 'Rodrigo Prieto',
    city: 'SAO PAULO',
    transactionId: 'DOTRIGO1', //max 25 characters
    message: 'Pagamento dotrigo pedito 1',
    cep: '05351015',
    value: 0,
  });

  imagem: string = '';

  async ngOnInit(): Promise<void> {
    this.service.listarProdutos().subscribe({
      next: (produtos) => {
        this.produtos.set(produtos);
      },
    });

    this.enumeradorService
      .listar()
      .pipe(take(1))
      .subscribe({
        next: (enumeradores) => {
          this.categorias.set(enumeradores.filter((e) => e.referenciaId === 2));
        },
      });

    this.carregarDadosComprador();
  }

  // Efeito para salvar o carrinho no localStorage sempre que ele mudar
  salvarCarrinhoEffect = effect(() => {
    try {
      localStorage.setItem('dotrigo_carrinho', JSON.stringify(this.carrinho()));
    } catch (e) {
      console.error('Não foi possível salvar o carrinho no localStorage.', e);
    }
  });

  // --- Sinais Computados ---

  // Filtra produtos com base na categoria ativa
  produtosFiltrados = computed(() => {
    const categoria = this.categoriaAtiva();
    if (categoria === 9) {
      return this.produtos();
    }
    return this.produtos().filter((p) => p.categoriaId === categoria);
  });

  // Filtra produtos para a seção "Pronta Entrega"
  produtosProntaEntrega = computed(() => {
    return this.produtos().filter((p) => p.prontaEntrega);
  });

  // Calcula o subtotal do carrinho
  subtotalCarrinho = computed(() => {
    return this.carrinho().reduce((total, item) => total + item.produto.preco * item.quantidade, 0);
  });

  // Calcula o total (pode incluir taxas futuras)
  totalCarrinho = computed(() => {
    return this.subtotalCarrinho(); // Por enquanto, total é igual ao subtotal
  });

  // Calcula a quantidade total de itens no carrinho
  totalItensCarrinho = computed(() => {
    return this.carrinho().reduce((total, item) => total + item.quantidade, 0);
  });

  // --- Métodos / Ações ---

  // Navega entre as páginas do app
  navegarPara(pagina: Pagina) {
    this.paginaAtual.set(pagina);
    window.scrollTo(0, 0); // Rola para o topo ao mudar de página

    // Limpa o status do pedido ao sair do checkout (exceto se for para confirmação)
    if (pagina !== 'checkout' && pagina !== 'confirmacao') {
      this.statusPedido.set('idle');
    }
  }

  // Define o filtro de categoria
  filtrarCategoria(categoria: number) {
    this.categoriaAtiva.set(categoria);
  }

  // Carrega o carrinho do localStorage ao iniciar
  carregarCarrinho(): PedidoItem[] {
    try {
      const carrinhoSalvo = localStorage.getItem('dotrigo_carrinho');
      return carrinhoSalvo ? JSON.parse(carrinhoSalvo) : [];
    } catch (e) {
      console.error('Não foi possível carregar o carrinho do localStorage.', e);
      return [];
    }
  }

  // Carrega o carrinho do localStorage ao iniciar
  carregarDadosComprador() {
    if (localStorage.getItem('dotrigo_comprador')) {
      try {
        const item = localStorage.getItem('dotrigo_comprador');
        const pedido = JSON.parse(item!);
        this.clienteNome.set(pedido.clienteNome);
        this.clienteTelefone.set(pedido.clienteTelefone);
        this.clienteEndereco.set(pedido.clienteEndereco);
        this.clienteEmail.set(pedido.clienteEmail);
      } catch (e) {
        console.error('Não foi possível carregar o carrinho do localStorage.', e);
      }
    }
    return true;
  }

  // Adiciona um produto ao carrinho
  adicionarAoCarrinho(produto: Produto, quantidade: number) {
    if (isNaN(quantidade) || quantidade <= 0) {
      quantidade = 1;
    }

    // Verifica se o produto já existe no carrinho
    const itemExistente = this.carrinho().find((item) => item.produto.id === produto.id);

    if (itemExistente) {
      // Se existe, atualiza a quantidade
      this.carrinho.update((carrinhoAtual) =>
        carrinhoAtual.map((item) =>
          item.id === itemExistente.id
            ? { ...item, quantidade: item.quantidade + quantidade }
            : item
        )
      );
    } else {
      // Se não existe, adiciona novo item
      const novoItem: PedidoItem = {
        id: produto.id, // ID único para o item no carrinho
        produto: produto,
        quantidade: quantidade,
        precoUnitario: produto.preco,
        nomeProduto: produto.nome,
        pedido: null,
        pedidoId: 0,
        produtoId: produto.id,
      };
      this.carrinho.update((carrinhoAtual) => [...carrinhoAtual, novoItem]);
    }

    // Feedback visual rápido (opcional)
    console.log(`${quantidade}x ${produto.nome} adicionado(s) ao carrinho.`);

    // Navega para o carrinho após adicionar (comportamento comum em mobile)
    this.navegarPara('carrinho');
  }

  // Remove um item do carrinho
  removerDoCarrinho(itemId: number) {
    this.carrinho.update((carrinhoAtual) => carrinhoAtual.filter((item) => item.id !== itemId));
  }

  // Atualiza a quantidade de um item
  atualizarQuantidade(itemId: number, novaQuantidade: number) {
    if (novaQuantidade <= 0) {
      // Remove o item se a quantidade for 0 ou menor
      this.removerDoCarrinho(itemId);
    } else {
      this.carrinho.update((carrinhoAtual) =>
        carrinhoAtual.map((item) =>
          item.id === itemId ? { ...item, quantidade: novaQuantidade } : item
        )
      );
    }
  }

  // Limpa o carrinho e os dados do formulário
  limpaTudo() {
    this.carrinho.set([]);
    this.clienteNome.set('');
    this.clienteTelefone.set('');
    this.clienteComentarios.set('');
    this.statusPedido.set('idle');
  }

  // Simula o envio do pedido para o backend .NET
  finalizarPedido(event: Event) {
    event.preventDefault(); // Impede o reload da página
    this.statusPedido.set('enviando');

    // 1. Montar o payload do pedido (o que será enviado para a API .NET)
    const pedidoParaEnviar: Pedido = {
      clienteNome: this.clienteNome(),
      clienteTelefone: this.clienteTelefone(),
      comentarios: this.clienteComentarios(),
      clienteEmail: this.clienteEmail(),
      valorTotal: this.totalCarrinho(),
      itens: this.carrinho().map((item) => item),
      statusId: null,
      status: null,
      dataPedido: new Date().toISOString(),
      id: 0,
      clienteEndereco: this.clienteEndereco(),
    };

    try {
      localStorage.setItem('dotrigo_comprador', JSON.stringify(pedidoParaEnviar));
    } catch (e) {
      console.error('Não foi possível salvar os dados no localStorage.', e);
    }

    // 2. Simular a chamada da API (HttpClient.post)
    // No app real:
    this.pedidoService
      .criarPedido(pedidoParaEnviar)
      .pipe(take(1))
      .subscribe({
        next: async (resp) => {
          this.qrCodePix = QrCodePix({
            version: '01',
            key: '0e1e80a5-f2b0-4709-b123-7bf87d2f714d',
            name: 'Rodrigo Prieto',
            city: 'SAO PAULO',
            transactionId: 'DOTRIGO' + resp.id.toString(), //max 25 characters
            message: 'Pagamento dotrigo pedido número: ' + resp.id.toString(),
            cep: '05351015',
            value: resp.valorTotal,
          });
          this.imagem = await this.qrCodePix.base64();

          setTimeout(() => {
            this.statusPedido.set('sucesso');
            this.ultimoPedido.set({
              id: resp.id.toString(),
              clienteNome: this.clienteNome(),
            });
            this.navegarPara('confirmacao');

            // Limpar o carrinho DEPOIS de navegar e salvar os dados
            this.carrinho.set([]);
            //this.clienteNome.set('');
            //this.clienteTelefone.set('');
            //this.clienteEndereco.set('');
            this.clienteComentarios.set('');
          }, 500);
        },
        error: (err) => {
          // Simular erro
          this.statusPedido.set('erro');
          this.toasterService.error('Erro ao enviar pedido:', err);
          console.error('Erro ao enviar pedido:', err);
        },
      });
  }
}
