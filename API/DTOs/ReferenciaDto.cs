using System.ComponentModel.DataAnnotations;

namespace API.Domain;

public class ReferenciaDto
{
	public int Id { get; set; }

	[StringLength(100)]
	public string Nome { get; set; }

	public bool Status { get; set; } = true;
}
