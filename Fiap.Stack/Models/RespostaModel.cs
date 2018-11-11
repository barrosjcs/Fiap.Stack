using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.Models
{
    public class RespostaModel
    {
        public int Codigo { get; set; }
        public int CodigoPergunta { get; set; }
        public int Votos { get; set; }
        public DateTime DataHoraCadastro { get; set; }

        [Required(ErrorMessage = Mensagem.ValidacaoDescricaoRespostaObrigatoria)]
        public string Descricao { get; set; }

        public static explicit operator RespostaMOD(RespostaModel resposta)
        {
            return new RespostaMOD
            {
                Codigo = resposta.Codigo,
                CodigoPergunta = resposta.CodigoPergunta,
                Votos = resposta.Votos,
                Descricao = resposta.Descricao
            };
        }
    }
}
