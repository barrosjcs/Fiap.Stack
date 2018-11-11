using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;
using Microsoft.Extensions.Configuration;

namespace Fiap.Stack.DAL
{
    public class TagDAL : ITagDAL
    {
        private readonly IConfiguration _configuration;

        public TagDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<int>> BuscarCodigosTagAsync(string[] descricoesTag)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	Codigo
                                FROM
                                	Tag
                                WHERE
                                	Descricao IN @Descricoes";

                return await connection.QueryAsync<int>(query, new { Descricoes = descricoesTag });
            }
        }

        public async Task<List<TagMOD>> BuscarTagsPorPerguntaAsync(int codigoPergunta)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	Tag.Codigo,
									Tag.Descricao
                                FROM
                                	Tag
									INNER JOIN PerguntaTag ON Tag.Codigo = PerguntaTag.CodigoTag
                                WHERE
                                	PerguntaTag.CodigoPergunta = @CodigoPergunta";

                return await connection.QueryAsync<TagMOD>(query, new { CodigoPergunta = codigoPergunta }) as List<TagMOD>;
            }
        }

        public async Task CadastrarTagAsync(TagMOD tag)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string insert = @"
                                INSERT INTO
                                    Tag
                                        (Descricao)
                                VALUES
                                    (@Descricao)";

                await connection.ExecuteAsync(insert, tag);
            }
        }

        public async Task<bool> VerificarExisteTagAsync(string descricaoTag)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	COUNT(*)
                                FROM
                                	Tag
                                WHERE
                                	Descricao = @Descricao";

                return await connection.QueryFirstOrDefaultAsync<bool>(query, new { Descricao = descricaoTag });
            }
        }
    }
}
