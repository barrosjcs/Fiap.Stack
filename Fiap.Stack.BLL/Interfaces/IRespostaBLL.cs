using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL.Interfaces
{
    public interface IRespostaBLL
    {
        Task<RespostaMOD> CadastrarRespostaAsync(RespostaMOD resposta);
    }
}
