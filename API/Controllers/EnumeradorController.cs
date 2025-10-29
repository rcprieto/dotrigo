using API.Domain.Interfaces.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumeradorController : ControllerBase
    {
        private readonly IEnumeradorRepository _enumeradorRepository;
        private readonly IMapper _mapper;

        public EnumeradorController(IEnumeradorRepository enumeradorRepository, IMapper mapper)
        {
            _enumeradorRepository = enumeradorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Enum>>> Get()
        {
            var items = _enumeradorRepository.GetAllAsync().Result.Where(c => c.Status).OrderBy(c => c.ReferenciaId).ThenBy(c => c.Ordem).ToList();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return BadRequest("Sem Items");
            }
        }
    }
}
