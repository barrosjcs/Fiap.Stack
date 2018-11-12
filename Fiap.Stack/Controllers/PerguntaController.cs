using System;
using System.Linq;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.Models;
using Fiap.Stack.MOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fiap.Stack.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class PerguntaController : ControllerBase
    {
        private readonly ITagBLL _tagBll;
        private readonly IPerguntaBLL _perguntaBll;
        private readonly IUsuarioBLL _usuarioBll;

        public PerguntaController(ITagBLL tagBll, IPerguntaBLL perguntaBll, IUsuarioBLL usuarioBll)
        {
            _tagBll = tagBll;
            _perguntaBll = perguntaBll;
            _usuarioBll = usuarioBll;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastrarPerguntaAsync(PerguntaModel pergunta)
        {
            try
            {
                var tagsMod = pergunta.Tags.Select(c => (TagMOD)c);
                await _tagBll.CadastarTagsAsync(tagsMod);
                var codigosTag = await _tagBll.RetornarCodigosTagAsync(tagsMod);

                var login = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                pergunta.CodigoUsuario = await _usuarioBll.RetornarCodigoUsuario(login);

                return Created(Request.Host.ToUriComponent(),
                    await _perguntaBll.CadastrarPerguntaAsync((PerguntaMOD)pergunta, codigosTag));
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Recente")]
        public async Task<IActionResult> RetornarPerguntasRecentesAsync()
        {
            try
            {
                return Ok(await _perguntaBll.RetornarPerguntasRecentesAsync());
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{codigo}")]
        public async Task<IActionResult> RetornarPerguntaPorCodigoAsync(int codigo)
        {
            try
            {
                return Ok(await _perguntaBll.RetornarPerguntaPorCodigoAsync(codigo));
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }
    }
}