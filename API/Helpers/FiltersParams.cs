using System;

namespace API.Domain.Helpers;

public class FiltersParams : PaginationParams
{
	public int Ano { get; set; } = DateTime.Now.Year;
	public int Mes { get; set; } = DateTime.Now.Month;

	public string UserId { get; set; } = "";


}
