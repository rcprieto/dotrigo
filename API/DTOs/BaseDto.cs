using System;

namespace API.Domain.DTOs;

public class BaseDto
{
	public int Id { get; set; }
	public string Nome { get; set; }

	public string NomeEs { get; set; }

	public string NomeEn { get; set; }
	public string NomeFr { get; set; }
}
