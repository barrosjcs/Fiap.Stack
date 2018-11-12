using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.MOD;
using Microsoft.Extensions.Configuration;

namespace Fiap.Stack.DAL
{
    public class UsuarioDAL : IUsuarioDAL
    {
        private readonly IConfiguration _configuration;

        public UsuarioDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> BuscarCodigoUsuario(string login)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	Codigo
                                FROM
                                	Usuario
                                WHERE
                                	Login = @Login";

                return await connection.QueryFirstOrDefaultAsync<int>(query, new { Login = login });
            }
        }

        public async Task<UsuarioMOD> BuscarUsuarioAsync(string login)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                    Codigo,
                                	Login,
                                    SenhaHash
                                FROM
                                	Usuario
                                WHERE
                                	Login = @Login";

                return await connection.QueryFirstOrDefaultAsync<UsuarioMOD>(query, new { Login = login });
            }
        }

        public async Task<UsuarioMOD> BuscarUsuarioAsync(int codigoUsuario)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string query = @"
                                SELECT
                                	Codigo,
                                	Nome,
                                	Login,
                                	Email,
                                	DataHoraCadastro
                                FROM
                                	Usuario
                                WHERE
                                	Codigo = @Codigo";

                return await connection.QueryFirstOrDefaultAsync<UsuarioMOD>(query, new { Codigo = codigoUsuario });
            }
        }

        public async Task<int> CadastrarUsuarioAsync(UsuarioMOD usuario)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Local")))
            {
                const string insert = @"
                                INSERT INTO
                                	Usuario
                                		(Nome, Email, Login, SenhaHash)
                                VALUES
                                	(@Nome, @Email, @Login, @SenhaHash)

                                SELECT @@IDENTITY";

                return await connection.QueryFirstOrDefaultAsync<int>(insert, usuario);
            }
        }
    }
}
