
using API.Domain.DTOs;
using API.Domain.Entidades;
using API.Domain.Interfaces.Repositories;

namespace API.Domain.Interfaces.Repository
{
    public interface IReferenciaRepository : IRepositoryBase<Referencia>
    {
        Task<List<DropdownDto>> GetDto(int lingua = 1);
    }
}
