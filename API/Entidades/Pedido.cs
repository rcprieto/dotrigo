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
	[Comment("Armazena cada pedido feito pelo cliente.")]
	[Index(nameof(Status), Name = "idx_status")]
	[Index(nameof(DataPedido), Name = "idx_dataPedido")]
	public class Pedido
	{
		/// <summary>
		/// ID numérico único do pedido (Auto-incremento).
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[Column("ref_id")]
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

		/// <summary>
		/// Status atual do pedido (ex: Recebido, Em Preparo).
		/// </summary>
		[Required]
		public Enumerador Status { get; set; }

		/// <summary>
		/// Data e hora em que o pedido foi recebido.
		/// </summary>
		public DateTime DataPedido { get; set; } = DateTime.Now;

		// Propriedade de navegação para os itens do pedido (relação 1-N)
		public virtual ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
	}
}
