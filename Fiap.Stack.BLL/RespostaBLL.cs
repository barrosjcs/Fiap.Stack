using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL
{
    public class RespostaBLL : IRespostaBLL
    {
        private readonly IRespostaDAL _respostaDal;

        public RespostaBLL(IRespostaDAL respostaDal)
        {
            _respostaDal = respostaDal;
        }

        public async Task<RespostaMOD> CadastrarRespostaAsync(RespostaMOD resposta)
        {
            var codigoResposta = await _respostaDal.CadastrarRespostaAsync(resposta);

            return await _respostaDal.BuscarRespostaAsync(codigoResposta);
        }
    }
}
