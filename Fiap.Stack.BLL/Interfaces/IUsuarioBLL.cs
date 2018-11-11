using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL.Interfaces
{
    public interface IUsuarioBLL
    {
        Task AutenticarUsuarioAsync(UsuarioMOD usuario);
        Task<UsuarioMOD> CadastrarUsuarioAsync(UsuarioMOD usuario);
    }
}
