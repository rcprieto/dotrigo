
using API.Data.Context;
using API.Domain.DTOs;
using API.Domain.Entidades;
using API.Domain.Interfaces.Repository;

namespace API.Data.Repositories
{
    public class EnumeradorRepository : RepositoryBase<Enumerador>, IEnumeradorRepository
    {
        public EnumeradorRepository(DoTrigoDbContext context) : base(context)
        {
        }

        public async Task<List<DropdownDto>> GetDto(int lingua = 1, int refId = 0)
        {
            var retorno = (from d in Db.Enumeradors
                           where d.Status == true
                           && d.ReferenciaId == refId
                           select new DropdownDto
                           {
                               Id = d.Id.ToString(),
                               Nome = d.Nome,
                               IdFk = d.ReferenciaId.ToString(),
                               NomeFk = d.Referencia.Nome

                           }).OrderBy(c => c.Nome).ToList();

            return retorno;
        }


    }
}
