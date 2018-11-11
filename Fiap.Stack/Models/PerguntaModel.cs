using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fiap.Stack.MOD;

namespace Fiap.Stack.Models
{
    public class PerguntaModel
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataHoraCadastro  { get; set; }

        [Required(ErrorMessage = Mensagem.ValidacaoTituloPerguntaObrigatoria)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = Mensagem.ValidacaoDescricaoPerguntaObrigatoria)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = Mensagem.ValidacaoDescricaoTagObrigatoria)]
        public IEnumerable<TagModel> Tags { get; set; }

        public static explicit operator PerguntaMOD(PerguntaModel pergunta)
        {
            return new PerguntaMOD
            {
                Codigo = pergunta.Codigo,
                CodigoUsuario = pergunta.CodigoUsuario,
                Titulo = pergunta.Titulo,
                Descricao = pergunta.Descricao
            };
        } 
    }
}
