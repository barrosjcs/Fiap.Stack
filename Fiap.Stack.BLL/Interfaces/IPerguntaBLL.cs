using Fiap.Stack.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.Stack.BLL.Interfaces
{
    public interface IPerguntaBLL
    {
        Task<PerguntaMOD> CadastrarPerguntaAsync(PerguntaMOD pergunta, IEnumerable<int> codigosTag);
    }
}
