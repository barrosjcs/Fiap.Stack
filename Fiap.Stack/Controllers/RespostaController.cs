using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.Models;
using Fiap.Stack.MOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Stack.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class RespostaController : ControllerBase
    {
        private readonly IRespostaBLL _respostaBll;

        public RespostaController(IRespostaBLL respostaBll)
        {
            _respostaBll = respostaBll;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastrarRespostaAsync(RespostaModel resposta)
        {
            try
            {
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