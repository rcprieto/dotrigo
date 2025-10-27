using API.Domain.Auxiliares;
using API.Domain.DTOs;
using API.Domain.Entidades;
using AutoMapper;

namespace API.Domain.Helpers;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<Enumerador, EnumeradorDto>();
		CreateMap<EnumeradorDto, Enumerador>();
		CreateMap<Referencia, ReferenciaDto>();
		CreateMap<ReferenciaDto, Referencia>();
	}

}
