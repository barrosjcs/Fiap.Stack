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
        private readonly ITagDAL _tagDal;

        public PerguntaBLL(IPerguntaDAL perguntaDal, ITagDAL tagDal)
        {
            _perguntaDal = perguntaDal;
            _tagDal = tagDal;
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

        public async Task<PerguntaMOD> RetornarPerguntaPorCodigoAsync(int codigo)
        {
            var pergunta = await _perguntaDal.RetornarPerguntaPorCodigoAsync(codigo);

            pergunta.Tags = await _tagDal.BuscarTagsPorPerguntaAsync(pergunta.Codigo);

            return pergunta;
        }

        public async Task<IEnumerable<PerguntaMOD>> RetornarPerguntasRecentesAsync()
        {
            var perguntas = await _perguntaDal.BuscarPerguntasRecentesAsync();

            foreach (var pergunta in perguntas)
            {
                pergunta.Tags = await _tagDal.BuscarTagsPorPerguntaAsync(pergunta.Codigo);
            }

            return perguntas;
        }
    }
}
