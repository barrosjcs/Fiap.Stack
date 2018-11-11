using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL
{
    public class PerguntaBLL : IPerguntaBLL
    {
        private readonly IPerguntaDAL _perguntaDal;

        public PerguntaBLL(IPerguntaDAL perguntaDal)
        {
            _perguntaDal = perguntaDal;
        }

        public async Task<PerguntaMOD> CadastrarPerguntaAsync(PerguntaMOD pergunta, IEnumerable<int> codigosTag)
        {
            var codigoPergunta = await _perguntaDal.CadastrarPerguntaAsync(pergunta);

            foreach (var codigoTag in codigosTag)
            {
                await _perguntaDal.CadastrarPerguntaTagAsync(codigoPergunta, codigoTag);
            }

            return await _perguntaDal.BuscarPerguntaAsync(codigoPergunta);
        }
    }
}
