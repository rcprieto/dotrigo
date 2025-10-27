import {
  Component,
  ChangeDetectionStrategy,
  signal,
  computed,
  WritableSignal,
  effect,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// --- Interfaces de Dados ---
// Define a estrutura de um produto
interface Produto {
  id: string;
  nome: string;
  categoria: 'pizzas' | 'paes' | 'focaccias' | 'outros';
  fotoUrl: string;
  descricao: string;
  tamanho: string; // ex: "Pequena", "Média", "Grande", "Unidade"
  peso: string; // ex: "300g", "500g"
  preco: number;
  tempoPreparo: string; // ex: "20 min", "Pronto"
  prontaEntrega: boolean; // Para a seção "Disponível"
}

// Define a estrutura de um item no carrinho
interface ItemCarrinho {
  id: string; // ID único do item no carrinho (pode ser produto.id + timestamp)
  produto: Produto;
  quantidade: number;
}

// Define a estrutura do pedido a ser enviado para o backend
interface Pedido {
  clienteNome: string;
  clienteTelefone: string;
  comentarios: string;
  itens: {
    produtoId: string;
    quantidade: number;
    precoUnitario: number;
  }[];
  total: number;
}

type Pagina = 'menu' | 'carrinho' | 'checkout' | 'prontaEntrega' | 'confirmacao';
type CategoriaFiltro = Produto['categoria'] | 'todos';
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
export class Produtos {
  // --- Estado da Aplicação (Sinais) ---

  // Gerenciamento de Página
  paginaAtual: WritableSignal<Pagina> = signal('menu');

  // Filtro de Categoria
  categoriaAtiva: WritableSignal<CategoriaFiltro> = signal('todos');

  // Carrinho de Compras
  carrinho: WritableSignal<ItemCarrinho[]> = signal(this.carregarCarrinho());

  // Dados do Formulário de Checkout
  clienteNome: WritableSignal<string> = signal('');
  clienteTelefone: WritableSignal<string> = signal('');
  clienteComentarios: WritableSignal<string> = signal('');

  // Status do Envio do Pedido
  statusPedido: WritableSignal<StatusPedido> = signal('idle');
  ultimoPedido: WritableSignal<{ id: string; clienteNome: string }> = signal({
    id: '',
    clienteNome: '',
  });

  // Efeito para salvar o carrinho no localStorage sempre que ele mudar
  salvarCarrinhoEffect = effect(() => {
    try {
      localStorage.setItem('dotrigo_carrinho', JSON.stringify(this.carrinho()));
    } catch (e) {
      console.error('Não foi possível salvar o carrinho no localStorage.', e);
    }
  });

  // --- Dados Mock (Substituir pela API .NET) ---
  produtos: WritableSignal<Produto[]> = signal([
    {
      id: 'p1',
      nome: 'Pão Italiano (Filone)',
      categoria: 'paes',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Pão+Italiano',
      descricao: 'Pão rústico de casca grossa e miolo macio. Fermentação natural.',
      tamanho: 'Unidade',
      peso: '500g',
      preco: 18.0,
      tempoPreparo: 'Pronto',
      prontaEntrega: true,
    },
    {
      id: 'p2',
      nome: 'Baguete Francesa',
      categoria: 'paes',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Baguete',
      descricao: 'Crocante por fora, aerada por dentro. Perfeita para sanduíches.',
      tamanho: 'Unidade',
      peso: '300g',
      preco: 8.5,
      tempoPreparo: '10 min',
      prontaEntrega: false,
    },
    {
      id: 'piz1',
      nome: 'Pizza Margherita',
      categoria: 'pizzas',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Pizza+Margherita',
      descricao: 'Molho de tomate italiano, muçarela fresca, manjericão e azeite.',
      tamanho: 'Grande (8 fatias)',
      peso: '700g',
      preco: 55.0,
      tempoPreparo: '25 min',
      prontaEntrega: false,
    },
    {
      id: 'piz2',
      nome: 'Pizza Calabresa',
      categoria: 'pizzas',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Pizza+Calabresa',
      descricao: 'Calabresa fatiada, anéis de cebola e azeitonas pretas.',
      tamanho: 'Grande (8 fatias)',
      peso: '750g',
      preco: 58.0,
      tempoPreparo: '25 min',
      prontaEntrega: false,
    },
    {
      id: 'f1',
      nome: 'Focaccia de Alecrim e Sal Grosso',
      categoria: 'focaccias',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Focaccia',
      descricao: 'Massa alta e fofa, regada com azeite, alecrim fresco e sal grosso.',
      tamanho: 'Pedaço',
      peso: '200g',
      preco: 12.0,
      tempoPreparo: 'Pronto',
      prontaEntrega: true,
    },
    {
      id: 'f2',
      nome: 'Focaccia de Tomate Cereja',
      categoria: 'focaccias',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Focaccia+Tomate',
      descricao: 'Com tomates cereja assados e orégano.',
      tamanho: 'Pedaço',
      peso: '220g',
      preco: 14.0,
      tempoPreparo: '15 min',
      prontaEntrega: false,
    },
    {
      id: 'o1',
      nome: 'Croissant Amêndoas',
      categoria: 'outros',
      fotoUrl: 'https://placehold.co/300x300/f5e7c4/8c5a2b?text=Croissant',
      descricao: 'Massa folhada com recheio de creme de amêndoas.',
      tamanho: 'Unidade',
      peso: '110g',
      preco: 15.0,
      tempoPreparo: 'Pronto',
      prontaEntrega: true,
    },
  ]);

  // --- Sinais Computados ---

  // Filtra produtos com base na categoria ativa
  produtosFiltrados = computed(() => {
    const categoria = this.categoriaAtiva();
    if (categoria === 'todos') {
      return this.produtos();
    }
    return this.produtos().filter((p) => p.categoria === categoria);
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
  filtrarCategoria(categoria: CategoriaFiltro) {
    this.categoriaAtiva.set(categoria);
  }

  // Carrega o carrinho do localStorage ao iniciar
  carregarCarrinho(): ItemCarrinho[] {
    try {
      const carrinhoSalvo = localStorage.getItem('dotrigo_carrinho');
      return carrinhoSalvo ? JSON.parse(carrinhoSalvo) : [];
    } catch (e) {
      console.error('Não foi possível carregar o carrinho do localStorage.', e);
      return [];
    }
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
      const novoItem: ItemCarrinho = {
        id: `${produto.id}-${Date.now()}`, // ID único para o item no carrinho
        produto: produto,
        quantidade: quantidade,
      };
      this.carrinho.update((carrinhoAtual) => [...carrinhoAtual, novoItem]);
    }

    // Feedback visual rápido (opcional)
    console.log(`${quantidade}x ${produto.nome} adicionado(s) ao carrinho.`);

    // Navega para o carrinho após adicionar (comportamento comum em mobile)
    this.navegarPara('carrinho');
  }

  // Remove um item do carrinho
  removerDoCarrinho(itemId: string) {
    this.carrinho.update((carrinhoAtual) => carrinhoAtual.filter((item) => item.id !== itemId));
  }

  // Atualiza a quantidade de um item
  atualizarQuantidade(itemId: string, novaQuantidade: number) {
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
      total: this.totalCarrinho(),
      itens: this.carrinho().map((item) => ({
        produtoId: item.produto.id,
        quantidade: item.quantidade,
        precoUnitario: item.produto.preco,
      })),
    };

    console.log('Enviando pedido para o backend:', pedidoParaEnviar);

    // 2. Simular a chamada da API (HttpClient.post)
    // No app real:
    // this.http.post('/api/pedidos', pedidoParaEnviar).subscribe({
    //   next: (resposta) => {
    //     this.statusPedido.set('sucesso');
    //     this.ultimoPedido.set({ id: resposta.id, clienteNome: this.clienteNome() });
    //     this.limparTudo();
    //     this.navegarPara('confirmacao');
    //   },
    //   error: (err) => {
    //     console.error("Erro ao enviar pedido:", err);
    //     this.statusPedido.set('erro');
    //   }
    // });

    // Simulação com setTimeout:
    setTimeout(() => {
      // Simular sucesso
      this.statusPedido.set('sucesso');
      this.ultimoPedido.set({
        id: `DOTRIGO-${Math.floor(Math.random() * 10000)}`, // ID de pedido falso
        clienteNome: this.clienteNome(),
      });
      this.navegarPara('confirmacao');

      // Limpar o carrinho DEPOIS de navegar e salvar os dados
      this.carrinho.set([]);
      this.clienteNome.set('');
      this.clienteTelefone.set('');
      this.clienteComentarios.set('');

      // Simular erro (descomente para testar)
      // this.statusPedido.set('erro');
    }, 1500); // Simula 1.5s de delay da rede
  }
}
