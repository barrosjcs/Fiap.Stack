using System.ComponentModel.DataAnnotations;
using Fiap.Stack.MOD;

namespace Fiap.Stack.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = Mensagem.ValidacaoNomeUsuarioObrigatoria)]
        public string Nome { get; set; }
        [Required(ErrorMessage = Mensagem.ValidacaoLoginUsuarioObrigatoria)]
        public string Login { get; set; }
        [Required(ErrorMessage = Mensagem.ValidacaoSenhaUsuarioObrigatoria)]
        public string Senha { get; set; }
        [Required(ErrorMessage = Mensagem.ValidacaoEmailUsuarioObrigatoria)]
        public string Email { get; set; }

        public static explicit operator UsuarioMOD(UsuarioModel usuario)
        {
            return new UsuarioMOD
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Login = usuario.Login,
                Senha = usuario.Senha
            };
        }
    }
}
