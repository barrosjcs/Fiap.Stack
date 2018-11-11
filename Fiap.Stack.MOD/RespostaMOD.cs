using System;

namespace Fiap.Stack.MOD
{
    public class RespostaMOD
    {
        public int Codigo { get; set; }
        public int CodigoPergunta { get; set; }
        public int CodigoUsuario { get; set; }
        public string Descricao { get; set; }
        public int Votos { get; set; }
        public DateTime DataHoraCadastro { get; set; }

        public UsuarioMOD Usuario { get; set; }
    }
}
