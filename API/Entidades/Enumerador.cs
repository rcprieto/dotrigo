using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entidades
{
    /// <summary>
    /// Itens para diversos cadastros
    /// </summary>
    [Table("tb_enum", Schema = "ws_dotrigo")]
    public class Enumerador
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("enum_id")]
        public int Id { get; set; }

        [Column("enum_nome")]
        [StringLength(250)]
        public string Nome { get; set; }


        [Column("ref_id")]
        public int? ReferenciaId { get; set; }

        [ForeignKey("ReferenciaId")]
        public Referencia Referencia { get; set; }


        [Column("enum_status")]
        public bool Status { get; set; } = true;

        [Column("enum_ordem")]
        public int? Ordem { get; set; }


    }
}
