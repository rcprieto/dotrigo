using System.ComponentModel.DataAnnotations.Schema;
using API.Domain.DTOs;

namespace API.Domain;

public class EnumeradorDto : BaseDto
{
	public int? ReferenciaId { get; set; }

	public ReferenciaDto Referencia { get; set; }


	public bool Status { get; set; } = true;

	public int? Ordem { get; set; }

}
