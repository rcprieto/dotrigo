export interface Enumerador {
  id: number;
  nome: string;
  referenciaId: number | null;
  referencia: Referencia;
  status: boolean;
  ordem: number | null;
}

export interface Pedido {
  id: number;
  clienteNome: string;
  clienteTelefone: string;
  comentarios: string | null;
  valorTotal: number;
  statusId: number | null;
  status: Enumerador;
  dataPedido: string;
  itens: PedidoItem[];
}

export interface PedidoItem {
  id: number;
  pedidoId: number;
  produtoId: number;
  quantidade: number;
  precoUnitario: number;
  nomeProduto: string;
  pedido: Pedido;
  produto: Produto;
}

export interface Produto {
  id: number;
  nome: string;
  categoriaId: number | null;
  categoria: Enumerador;
  fotoUrl: string | null;
  descricao: string | null;
  tamanho: string | null;
  peso: string | null;
  preco: number;
  tempoPreparo: string | null;
  prontaEntrega: boolean;
  ativo: boolean;
  dataCadastro: string;
  pedidoItens: PedidoItem[];
}

export interface Referencia {
  id: number;
  nome: string;
  status: boolean;
}
