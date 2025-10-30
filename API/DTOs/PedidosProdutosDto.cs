using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public record PedidoItemDto(
	   [Required]
		int ProdutoId,

	   [Range(1, 100)]
		int Quantidade
   );

/// <summary>
/// DTO para a criação de um novo pedido.
/// Contém os dados do cliente e a lista de itens.
/// </summary>
public record CriarPedidoDto(
	[Required]
		[StringLength(255)]
		string ClienteNome,

	[Required]
		[StringLength(50)]
		string ClienteTelefone,

	string? Comentarios,

	[Required]
		[MinLength(1)]
		List<PedidoItemDto> Itens
);

/// <summary>
/// DTO para retornar uma resposta de erro padronizada.
/// </summary>
public record ErroDto(
	string Mensagem
);

public class PixPayload
{
	public string PixKey { get; set; } // Chave Pix (CPF, e-mail, etc.)
	public string ReceiverName { get; set; } // Nome do recebedor
	public string ReceiverCity { get; set; } // Cidade do recebedor
	public decimal? Value { get; set; } // Valor (opcional)
	public string TransactionId { get; set; } // ID da transação (opcional)
	public string Message { get; set; } // Mensagem (opcional)
}
