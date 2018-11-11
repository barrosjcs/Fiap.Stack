using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;

namespace Fiap.Stack.BLL
{
    public class TagBLL : ITagBLL
    {
        private readonly ITagDAL _tagDal;

        public TagBLL(ITagDAL tagDal)
        {
            _tagDal = tagDal;
        }

        public async Task CadastarTagsAsync(IEnumerable<TagMOD> tags)
        {
            foreach (var tag in tags)
            {
                var existe = await _tagDal.VerificarExisteTagAsync(tag.Descricao.ToLowerInvariant());

                if (!existe)
                    await _tagDal.CadastrarTagAsync(new TagMOD
                    {
                        Descricao = tag.Descricao.ToLowerInvariant()
                    });
            }
        }

        public async Task<IEnumerable<int>> RetornarCodigosTagAsync(IEnumerable<TagMOD> tags)
        {
            return await _tagDal.BuscarCodigosTagAsync(tags.Select(c => c.Descricao).ToArray());
        }
    }
}
