using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entidades
{
    /// <summary>
    /// Referencia para os Enum, ex Tipo de Condensação
    /// </summary>
    [Table("tb_referencia", Schema = "ws_dotrigo")]
    public class Referencia
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ref_id")]
        public int Id { get; set; }

        [Column("ref_nome")]
        [StringLength(100)]
        public string Nome { get; set; }


        [Column("ref_status")]
        public bool Status { get; set; } = true;

    }
}
