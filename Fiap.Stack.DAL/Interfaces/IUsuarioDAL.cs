using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.DAL.Interfaces
{
    public interface IUsuarioDAL
    {
        Task<int> BuscarCodigoUsuario(string login);
        Task<int> CadastrarUsuarioAsync(UsuarioMOD usuario);
        Task<UsuarioMOD> BuscarUsuarioAsync(string login);
        Task<UsuarioMOD> BuscarUsuarioAsync(int codigoUsuario);
    }
}
