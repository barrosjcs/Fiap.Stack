using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL.Interfaces
{
    public interface IUsuarioBLL
    {
        Task<int> RetornarCodigoUsuario(string login);
        Task AutenticarUsuarioAsync(UsuarioMOD usuario);
        Task<UsuarioMOD> CadastrarUsuarioAsync(UsuarioMOD usuario);
    }
}
