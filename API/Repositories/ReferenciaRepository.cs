

using API.Data.Context;
using API.Domain.DTOs;
using API.Domain.Entidades;
using API.Domain.Interfaces.Repository;

namespace API.Data.Repositories
{
    public class ReferenciaRepository : RepositoryBase<Referencia>, IReferenciaRepository
    {
        public ReferenciaRepository(DoTrigoDbContext context) : base(context)
        {
        }

        public async Task<List<DropdownDto>> GetDto(int lingua = 1)
        {
            var retorno = (from d in Db.Referencias
                           where d.Status == true
                           select new DropdownDto
                           {
                               Id = d.Id.ToString(),
                               Nome = d.Nome
                           }).OrderBy(c => c.Nome).ToList();

            return retorno;
        }
    }
}
