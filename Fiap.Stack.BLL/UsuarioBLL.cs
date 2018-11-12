using System;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.BLL.Util;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL
{
    public class UsuarioBLL : IUsuarioBLL
    {
        private readonly IUsuarioDAL _usuarioDal;

        public UsuarioBLL(IUsuarioDAL usuarioDal)
        {
            _usuarioDal = usuarioDal;
        }

        public async Task AutenticarUsuarioAsync(UsuarioMOD usuario)
        {
            var usuarioBanco = await _usuarioDal.BuscarUsuarioAsync(usuario.Login);

            var autenticado = Criptografia.Cript(usuario.Senha) == usuarioBanco.SenhaHash;

            if (!autenticado)
                throw new InvalidOperationException(Mensagem.CredenciaisInvalidas);
        }

        public async Task<UsuarioMOD> CadastrarUsuarioAsync(UsuarioMOD usuario)
        {
            usuario.SenhaHash = Criptografia.Cript(usuario.Senha);

            var codigoUsuario = await _usuarioDal.CadastrarUsuarioAsync(usuario);

            return await _usuarioDal.BuscarUsuarioAsync(codigoUsuario);
        }

        public async Task<int> RetornarCodigoUsuario(string login)
        {
            return await _usuarioDal.BuscarCodigoUsuario(login);
        }
    }
}
