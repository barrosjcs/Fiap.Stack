using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.Stack.Models
{
    public class RetornoModel
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public RetornoModel() { }

        public RetornoModel(string mensagem)
        {
            Sucesso = false;
            Mensagem = mensagem;
        }
    }
}
