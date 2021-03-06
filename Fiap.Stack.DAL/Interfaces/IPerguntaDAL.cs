﻿using Fiap.Stack.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.Stack.DAL.Interfaces
{
    public interface IPerguntaDAL
    {
        Task<int> CadastrarPerguntaAsync(PerguntaMOD pergunta);
        Task CadastrarPerguntaTagAsync(int codigoPergunta, int codigoTag);
        Task<PerguntaMOD> BuscarPerguntaAsync(int codigoPergunta);
        Task<IEnumerable<PerguntaMOD>> BuscarPerguntasRecentesAsync();
        Task<PerguntaMOD> RetornarPerguntaPorCodigoAsync(int codigo);
    }
}
