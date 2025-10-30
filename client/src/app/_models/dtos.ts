import { Produto } from './entidades';

export interface BaseDto {
  id: number;
  nome: string;
}

export interface DropdownDto {
  id: string;
  idFk: string;
  nome: string;
  nomeFk: string;
  selected: boolean;
}

export interface EnumeradorDto extends BaseDto {
  referenciaId: number | null;
  referencia: ReferenciaDto;
  status: boolean;
  ordem: number | null;
}

export interface ReferenciaDto {
  id: number;
  nome: string;
  status: boolean;
}

export interface PedidoItemDto {
  produtoId: number;
  quantidade: number;
}

export interface CriarPedidoDto {
  clienteNome: string;
  clienteTelefone: string;
  itens: PedidoItemDto[];
}

export interface ErrorDto {
  mensagem: string;
}

export interface ItemCarrinho {
  id: string; // ID único do item no carrinho (pode ser produto.id + timestamp)
  produto: Produto;
  quantidade: number;
}

export interface SalvarArquivosDto {
  arquivos: File[];
  id: number;
}
