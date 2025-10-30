using System.Drawing.Imaging;
using API.Domain;
using API.Domain.Auxiliares;
using API.Domain.DTOs;
using API.Domain.Entidades;
using API.Domain.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private static readonly string[] ExtensoesPermitidas =
          { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

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


        [HttpPost]
        public async Task<ActionResult<Produto>> Add([FromBody] Produto model)
        {
            Geral.SanatizeClass(model);
            //var item = _mapper.Map<Area>(model);
            model.DataCadastro = DateTime.Now;


            await _produtoRepository.AddAsync(model);
            if (_produtoRepository.SaveChangesAsync().Result == -1)
                return BadRequest("Erro ao salvar");

            //model.Id = item.Id;

            return Ok(model);

        }

        [HttpPut]
        public async Task<ActionResult<Produto>> Update(Produto model)
        {
            Geral.SanatizeClass(model);
            model.DataCadastro = DateTime.Now;
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

        [HttpPost("salvar-foto")]
        public async Task<ActionResult> SaveArquivo(SalvarArquivosDto model, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (model.Arquivos == null || model.Arquivos.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            // 2. Define o caminho de destino (ex: .../wwwroot/fotos)
            string caminhoDestinoBase = Path.Combine(webHostEnvironment.WebRootPath, "assets/fotos");

            try
            {
                // 3. Garante que o diretório de destino exista
                if (!Directory.Exists(caminhoDestinoBase))
                {
                    Directory.CreateDirectory(caminhoDestinoBase);
                }
            }
            catch (Exception ex)
            {
                // Retorna um erro 500 se não conseguir criar a pasta (ex: permissões)
                return StatusCode(500, $"Erro crítico ao criar diretório: {ex.Message}");
            }


            foreach (var arquivo in model.Arquivos)
            {
                if (arquivo == null || arquivo.Length == 0)
                {
                    continue; // Pula arquivos nulos ou vazios
                }

                // 4. Validação de Segurança (Extensão)
                var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !ExtensoesPermitidas.Contains(ext))
                {
                    // Retorna 400 (Bad Request) se a extensão não for permitida
                    return BadRequest($"Extensão de arquivo não permitida: {ext}. Permitidas: {string.Join(", ", ExtensoesPermitidas)}");
                }

                // 5. Validação de Segurança (Nome de Arquivo Único)
                // Isso evita substituição de arquivos e ataques de path traversal.
                string nomeArquivoUnico = $"{model.Id}{ext}";
                string caminhoCompletoArquivo = Path.Combine(caminhoDestinoBase, nomeArquivoUnico);


                var imagem = System.Drawing.Image.FromStream(arquivo.OpenReadStream(), true, true);
                var novaImagem = ResizeImage.Resize(imagem, 700);
                var format = ImageFormat.Jpeg;

                if (ext.ToUpper().Contains("GIF"))
                    format = ImageFormat.Gif;
                else if (ext.ToUpper().Contains("PNG"))
                    format = ImageFormat.Png;







                try
                {
                    // 6. Salva o arquivo no disco (assincronamente)
                    // 'await using' garante que o stream será fechado corretamente.
                    using (var stream = new FileStream(caminhoCompletoArquivo, FileMode.Create))
                    {
                        //await arquivo.CopyToAsync(stream);
                        novaImagem.Save(stream, format);
                    }





                    var produto = _produtoRepository.GetByIdAsync(model.Id).Result;
                    produto.FotoUrl = nomeArquivoUnico;
                    _produtoRepository.Update(produto);

                    // 7. Adiciona o caminho relativo (útil para o frontend) à lista
                }
                catch (Exception ex)
                {
                    // Retorna um erro 500 se falhar ao salvar este arquivo
                    return StatusCode(500, $"Erro ao salvar o arquivo '{arquivo.FileName}': {ex.Message}");
                }
            }

            return Ok();

        }


        public class SalvarArquivosDto
        {
            public IFormFile[] Arquivos { get; set; }
            public int Id { get; set; } = 0;

        }


    }
}
