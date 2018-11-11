using System.Collections.Generic;
using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.DAL.Interfaces
{
    public interface ITagDAL
    {
        Task CadastrarTagAsync(TagMOD tag);
        Task<bool> VerificarExisteTagAsync(string descricaoTag);
        Task<IEnumerable<int>> BuscarCodigosTagAsync(string[] descricoesTag);
    }
}
