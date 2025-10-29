using API.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entidades
{
	/// <summary>
	/// Armazena as informações principais de cada pedido (cliente, totais).
	/// </summary>
	[Table("tb_pedido", Schema = "ws_dotrigo")]
	public class Pedido
	{
		/// <summary>
		/// ID numérico único do pedido (Auto-incremento).
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Nome do cliente que fez o pedido.
		/// </summary>
		[Required]
		[StringLength(255)]
		public string ClienteNome { get; set; } = string.Empty;

		/// <summary>
		/// Telefone/WhatsApp do cliente.
		/// </summary>
		[Required]
		[StringLength(50)]
		public string ClienteTelefone { get; set; } = string.Empty;

		/// <summary>
		/// Observações adicionadas pelo cliente.
		/// </summary>
		public string? Comentarios { get; set; }

		/// <summary>
		/// Valor total do pedido, calculado no backend.
		/// </summary>
		[Required]
		public decimal ValorTotal { get; set; }

		[Column("enum_id")]
		public int? StatusId { get; set; }

		[ForeignKey("StatusId")]
		public Enumerador Status { get; set; }

		/// <summary>
		/// Data e hora em que o pedido foi recebido.
		/// </summary>
		public DateTime DataPedido { get; set; } = DateTime.Now;

		// Propriedade de navegação para os itens do pedido (relação 1-N)
		public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
	}
}
