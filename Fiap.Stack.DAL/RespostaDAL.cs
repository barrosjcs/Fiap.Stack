using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;
using Microsoft.Extensions.Configuration;

namespace Fiap.Stack.DAL
{
    public class RespostaDAL : IRespostaDAL
    {
        private readonly IConfiguration _configuration;

        public RespostaDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<RespostaMOD> BuscarRespostaAsync(int codigoResposta)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	Codigo,
                                	CodigoPergunta,
                                    CodigoUsuario,
                                	Descricao,
                                	Votos,
                                	DataHoraCadastro
                                FROM
                                	Resposta
                                WHERE
                                	Codigo = @Codigo";

                return await connection.QueryFirstOrDefaultAsync<RespostaMOD>(query, new { Codigo = codigoResposta });
            }
        }

        public async Task<int> CadastrarRespostaAsync(RespostaMOD resposta)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string insert = @"
                                INSERT INTO
                                	Resposta
                                		(CodigoPergunta, CodigoUsuario, Descricao, Votos)
                                VALUES
                                	(@CodigoPergunta, @CodigoUsuario, @Descricao, @Votos)

                                SELECT @@IDENTITY";

                return await connection.QueryFirstOrDefaultAsync<int>(insert, resposta);
            }
        }
    }
}
