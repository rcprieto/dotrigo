using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entidades
{
	/// <summary>
	/// Tabela de ligação que detalha os produtos de um pedido específico.
	/// </summary>
	[Table("tb_pedido_item", Schema = "ws_dotrigo")]
	[Comment("Itens individuais de cada pedido.")]
	[Index(nameof(PedidoId), Name = "idx_pedidoId")]
	[Index(nameof(ProdutoId), Name = "idx_produtoId")]
	public class PedidoItem
	{
		/// <summary>
		/// ID único do item (Auto-incremento).
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[Column("ref_id")]
		public int Id { get; set; }

		/// <summary>
		/// ID do pedido ao qual este item pertence.
		/// </summary>
		[Required]
		public int PedidoId { get; set; }

		/// <summary>
		/// ID do produto comprado.
		/// </summary>
		[Required]
		[StringLength(50)]
		public string ProdutoId { get; set; } = string.Empty;

		/// <summary>
		/// Quantidade comprada deste produto.
		/// </summary>
		[Required]
		public int Quantidade { get; set; }

		/// <summary>
		/// Preço do produto no momento da compra (snapshot).
		/// </summary>
		[Required]
		[Comment("Preço do produto no momento da compra")]
		public decimal PrecoUnitario { get; set; }

		/// <summary>
		/// Nome do produto no momento da compra (snapshot).
		/// </summary>
		[Required]
		[StringLength(255)]
		[Comment("Nome do produto no momento da compra")]
		public string NomeProduto { get; set; } = string.Empty;


		// Propriedades de Navegação (Chaves Estrangeiras)

		/// <summary>
		/// Navegação para o pedido "pai".
		/// </summary>
		[ForeignKey(nameof(PedidoId))]
		public virtual Pedido Pedido { get; set; } = null!;

		/// <summary>
		/// Navegação para o produto de catálogo.
		/// </summary>
		[ForeignKey(nameof(ProdutoId))]
		public virtual Produto Produto { get; set; } = null!;
	}
}
