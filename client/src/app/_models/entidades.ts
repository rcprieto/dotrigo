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
  status: Enumerador | null;
  dataPedido: string;
  itens: PedidoItem[];
  clienteEndereco: string | null;
  clienteEmail: string;
}

export interface PedidoItem {
  id: number;
  pedidoId: number;
  produtoId: number;
  quantidade: number;
  precoUnitario: number;
  nomeProduto: string;
  pedido: Pedido | null;
  produto: Produto;
}

export interface Produto {
  id: number;
  nome: string;
  categoriaId: number | null;
  categoria: Enumerador | null;
  fotoUrl: string | null;
  descricao: string | null;
  tamanho: string | null;
  peso: string | null;
  preco: number;
  tempoPreparo: string | null;
  prontaEntrega: boolean;
  ativo: boolean;
  dataCadastro: string | null;
  pedidoItens: PedidoItem[];
  estoque: number | null;
}

export interface Referencia {
  id: number;
  nome: string;
  status: boolean;
}
