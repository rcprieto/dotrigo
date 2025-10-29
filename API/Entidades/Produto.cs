using API.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entidades
{
	/// <summary>
	/// Representa um item do cardápio (catálogo de produtos).
	/// </summary>
	[Table("tb_produto", Schema = "ws_dotrigo")]
	public class Produto
	{
		/// <summary>
		/// ID único do produto (ex: 'p1', 'piz1').
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[Column("prod_id")]
		public int Id { get; set; }

		/// <summary>
		/// Nome de exibição do produto.
		/// </summary>
		[Required]
		[StringLength(255)]
		public string Nome { get; set; }


		[Column("enum_cat_id")]
		public int? CategoriaId { get; set; }

		[ForeignKey("CategoriaId")]
		public Enumerador Categoria { get; set; }

		/// <summary>
		/// URL da imagem do produto.
		/// </summary>
		[StringLength(1000)]
		public string? FotoUrl { get; set; }

		/// <summary>
		/// Descrição detalhada do produto.
		/// </summary>
		public string? Descricao { get; set; }

		/// <summary>
		/// Tamanho (ex: "Pequena", "Média", "Unidade").
		/// </summary>
		[StringLength(100)]
		[Comment("Ex: \"Pequena\", \"Média\", \"Grande\", \"Unidade\"")]
		public string? Tamanho { get; set; }

		/// <summary>
		/// Peso do produto (ex: "300g", "500g").
		/// </summary>
		[StringLength(50)]
		[Comment("Ex: \"300g\", \"500g\"")]
		public string? Peso { get; set; }

		/// <summary>
		/// Preço de venda do produto.
		/// </summary>
		[Required]
		public decimal Preco { get; set; }

		/// <summary>
		/// Tempo estimado de preparo (ex: "20 min", "Pronto").
		/// </summary>
		[StringLength(50)]
		[Comment("Ex: \"20 min\", \"Pronto\"")]
		public string? TempoPreparo { get; set; }

		/// <summary>
		/// Indica se o produto está disponível para pronta entrega.
		/// </summary>
		[Comment("1 = Sim, 0 = Não")]
		public bool ProntaEntrega { get; set; } = false;

		/// <summary>
		/// Indica se o produto está visível no menu.
		/// </summary>
		[Comment("1 = Visível no menu, 0 = Oculto")]
		public bool Ativo { get; set; } = true;

		/// <summary>
		/// Data de cadastro do produto.
		/// </summary>
		public DateTime? DataCadastro { get; set; }
		public int? Estoque { get; set; } = 0;

		// Propriedade de navegação para os itens de pedido (relação 1-N)
		public ICollection<PedidoItem> PedidoItens { get; set; } = new List<PedidoItem>();
	}
}
