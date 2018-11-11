using System;
using System.Collections.Generic;

namespace Fiap.Stack.MOD
{
    public class PerguntaMOD
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCadastro  { get; set; }

        public UsuarioMOD Usuario { get; set; }
        public List<TagMOD> Tags { get; set; }
    }
}
