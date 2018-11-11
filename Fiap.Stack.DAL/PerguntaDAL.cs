using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;
using System.Threading.Tasks;
using Dapper;

namespace Fiap.Stack.DAL
{
    public class PerguntaDAL : IPerguntaDAL
    {
        private readonly IConfiguration _configuration;

        public PerguntaDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CadastrarPerguntaAsync(PerguntaMOD pergunta)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string insert = @"
                                INSERT INTO
                                	Pergunta
                                		(CodigoUsuario, Descricao)
                                VALUES
                                	(@CodigoUsuario, @Descricao)

                                SELECT @@IDENTITY";

                return await connection.QueryFirstOrDefaultAsync<int>(insert, pergunta);
            }
        }

        public async Task CadastrarPerguntaTagAsync(int codigoPergunta, int codigoTag)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string insert = @"
                                INSERT INTO
                                	PerguntaTag
                                		(CodigoPergunta, CodigoTag)
                                VALUES
                                	(@CodigoPergunta, @CodigoTag)";

                await connection.ExecuteAsync(insert, new
                {
                    CodigoPergunta = codigoPergunta,
                    CodigoTag = codigoTag
                });
            }
        }

        public async Task<PerguntaMOD> BuscarPerguntaAsync(int codigoPergunta)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	Pergunta.Codigo,
                                	Pergunta.CodigoUsuario,
                                	Pergunta.Descricao,
                                	Pergunta.DataHoraCadastro,
                                	Tag.Codigo,
                                	Tag.Descricao
                                FROM
                                	Pergunta
                                	LEFT JOIN PerguntaTag ON Pergunta.Codigo = PerguntaTag.CodigoPergunta
                                	LEFT JOIN Tag ON PerguntaTag.CodigoTag = Tag.Codigo
                                WHERE
                                	Pergunta.Codigo = @Codigo";

                var lookup = new Dictionary<int, PerguntaMOD>();
                var lista = await connection.QueryAsync<PerguntaMOD, TagMOD, PerguntaMOD>(query,
                    (pergunta, tag) =>
                    {
                        if (!lookup.TryGetValue(pergunta.Codigo, out PerguntaMOD perguntaEntry))
                        {
                            perguntaEntry = pergunta;
                            perguntaEntry.Tags = new List<TagMOD>();
                            lookup.Add(perguntaEntry.Codigo, perguntaEntry);
                        }

                        if(tag != null)
                            perguntaEntry.Tags.Add(tag);

                        return perguntaEntry;
                    },
                    new
                    {
                        Codigo = codigoPergunta
                    },
                    splitOn: "Codigo");

                return lista.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<PerguntaMOD>> BuscarPerguntasRecentesAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT TOP 5
                                    Pergunta.Codigo,
                                    Pergunta.CodigoUsuario,
                                    Pergunta.Descricao,
                                    Pergunta.DataHoraCadastro,
                                	Usuario.Codigo,
                                	Usuario.Nome
                                FROM
                                    Pergunta
                                	INNER JOIN Usuario ON Pergunta.CodigoUsuario = Usuario.Codigo
                                ORDER BY
                                	DataHoraCadastro DESC";

                return await connection.QueryAsync<PerguntaMOD, UsuarioMOD, PerguntaMOD>(query,
                    (pergunta, usuario) =>
                    {
                        pergunta.Usuario = usuario;
                        return pergunta;
                    },
                    splitOn: "Codigo");
            }
        }
    }
}
