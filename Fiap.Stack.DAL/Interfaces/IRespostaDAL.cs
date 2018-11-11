using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.DAL.Interfaces
{
    public interface IRespostaDAL
    {
        Task<int> CadastrarRespostaAsync(RespostaMOD resposta);
        Task<RespostaMOD> BuscarRespostaAsync(int codigoResposta);
    }
}
