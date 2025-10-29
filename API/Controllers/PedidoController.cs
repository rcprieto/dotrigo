using API.Domain.Auxiliares;
using API.Domain.Entidades;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoController(IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get()
        {
            var items = _pedidoRepository.GetAllAsync().Result.Where(c => c.StatusId != 10).ToList();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return BadRequest("Sem Items");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pedido>>> Get(int id)
        {
            var items = _pedidoRepository.GetAllAsync().Result.Where(c => c.StatusId != 10 && c.Id == id).ToList();
            if (items != null)
            {

                return Ok(items);
            }
            else
            {
                return BadRequest("Sem Items");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Pedido>> Add([FromBody] Pedido model)
        {
            Geral.SanatizeClass(model);
            model.StatusId = 1;
            model.Status = null;

            foreach (var item in model.Itens)
            {
                item.Produto = null;
                item.Id = 0;

            }


            await _pedidoRepository.AddAsync(model);
            var retorno = _pedidoRepository.SaveChangesAsync().Result;
            if (retorno == -1)
                return BadRequest("Erro ao salvar");

            else
            {
                EmailService.EmailPedidoEnviado(model);
            }

            model.Itens = null;
            return Ok(model);

        }

        [HttpPut]
        public async Task<ActionResult<Pedido>> Update(Pedido model)
        {
            Geral.SanatizeClass(model);

            _pedidoRepository.Update(model);
            return Ok(model);


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var item = await _pedidoRepository.GetByIdAsync(id);
            if (item == null) return NotFound();

            item.StatusId = 10;
            _pedidoRepository.Update(item);

            return NoContent();

        }
    }
}
