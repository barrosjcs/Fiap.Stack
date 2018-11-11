using System.Collections.Generic;
using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL.Interfaces
{
    public interface ITagBLL
    {
        Task CadastarTagsAsync(IEnumerable<TagMOD> tags);
        Task<IEnumerable<int>> RetornarCodigosTagAsync(IEnumerable<TagMOD> tags);
    }
}
