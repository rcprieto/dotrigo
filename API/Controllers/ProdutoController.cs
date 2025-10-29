using API.Domain;
using API.Domain.Auxiliares;
using API.Domain.DTOs;
using API.Domain.Entidades;
using API.Domain.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var items = _produtoRepository.GetAllAsync().Result.Where(c => c.Ativo).ToList();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return BadRequest("Sem Items");
            }
        }




        // [HttpGet("get-drop")]
        // public async Task<ActionResult<List<DropdownDto>>> GetDrop()
        // {
        // 	var retorno = await _produtoRepository.GetDto();

        // 	if (retorno.Any())
        // 	{
        // 		return Ok(retorno);
        // 	}
        // 	else
        // 	{
        // 		return BadRequest("Sem Usuários");
        // 	}
        // }


        [HttpPost]
        public async Task<ActionResult<Produto>> Add([FromBody] Produto model)
        {
            Geral.SanatizeClass(model);
            //var item = _mapper.Map<Area>(model);

            await _produtoRepository.AddAsync(model);
            if (_produtoRepository.SaveChangesAsync().Result == 1)
                return BadRequest("Erro ao salvar");

            //model.Id = item.Id;

            return Ok(model);

        }

        [HttpPut]
        public async Task<ActionResult<Produto>> Update(Produto model)
        {
            Geral.SanatizeClass(model);
            //var item = await _produtoRepository.GetByIdAsync(model.Id);
            //if (item == null) return NotFound();

            _produtoRepository.Update(model);
            return Ok(model);


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var item = await _produtoRepository.GetByIdAsync(id);
            if (item == null) return NotFound();

            item.Ativo = false;
            _produtoRepository.Update(item);

            return NoContent();

        }





    }
}
