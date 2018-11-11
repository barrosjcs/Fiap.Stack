using Fiap.Stack.MOD;

namespace Fiap.Stack.Models
{
    public class TagModel
    {
        public string Descricao { get; set; }
        public static explicit operator TagMOD(TagModel tag) => new TagMOD { Descricao = tag.Descricao };
    }
}
