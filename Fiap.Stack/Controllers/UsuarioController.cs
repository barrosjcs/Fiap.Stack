using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.Models;
using Fiap.Stack.MOD;
using Fiap.Stack.Security;

namespace Fiap.Stack.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioBLL _usuarioBll;

        public UsuarioController(IUsuarioBLL usuarioBll)
        {
            _usuarioBll = usuarioBll;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuarioAsync(UsuarioModel usuario)
        {
            try
            {
                return Created(Request.Host.ToUriComponent(),
                    await _usuarioBll.CadastrarUsuarioAsync((UsuarioMOD)usuario));
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AutenticarUsuarioAsync(LoginModel usuario, [FromServices] TokenSettings tokenSettings)
        {
            try
            {
                await _usuarioBll.AutenticarUsuarioAsync((UsuarioMOD) usuario);

                return Ok(GenerateToken(usuario.Login, tokenSettings));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new RetornoModel(e.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, new RetornoModel(Mensagem.ErroPadrao));
            }
        }

        private string GenerateToken(string userName, TokenSettings tokenSettings)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, userName),
                new Claim(JwtRegisteredClaimNames.Iss, tokenSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, tokenSettings.Audience),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(tokenSettings.Hours)).ToUnixTimeSeconds().ToString()),
            };
            var text = Encoding.UTF8.GetBytes(tokenSettings.Key);
            var key = new SymmetricSecurityKey(text);
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credential);
            var payload = new JwtPayload(claims);
            var token = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}