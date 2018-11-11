using System;
using System.Linq;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.Models;
using Fiap.Stack.MOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Stack.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class PerguntaController : ControllerBase
    {
        private readonly ITagBLL _tagBll;
        private readonly IPerguntaBLL _perguntaBll;

        public PerguntaController(ITagBLL tagBll, IPerguntaBLL perguntaBll)
        {
            _tagBll = tagBll;
            _perguntaBll = perguntaBll;
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

                return Created(Request.Host.ToUriComponent(),
                    await _perguntaBll.CadastrarPerguntaAsync((PerguntaMOD)pergunta, codigosTag));
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }

        [HttpGet]
        [Route("Recente")]
        [AllowAnonymous]
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
    }
}