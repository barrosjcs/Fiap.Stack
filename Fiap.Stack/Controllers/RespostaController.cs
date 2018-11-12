using System;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.Models;
using Fiap.Stack.MOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace Fiap.Stack.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[Controller]")]
    public class RespostaController : ControllerBase
    {
        private readonly IRespostaBLL _respostaBll;
        private readonly IUsuarioBLL _usuarioBll;

        public RespostaController(IRespostaBLL respostaBll, IUsuarioBLL usuarioBll)
        {
            _respostaBll = respostaBll;
            _usuarioBll = usuarioBll;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastrarRespostaAsync(RespostaModel resposta)
        {
            try
            {
                var login = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                resposta.CodigoUsuario = await _usuarioBll.RetornarCodigoUsuario(login);

                return Created(Request.Host.ToUriComponent(),
                    await _respostaBll.CadastrarRespostaAsync((RespostaMOD) resposta));
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }
    }
}