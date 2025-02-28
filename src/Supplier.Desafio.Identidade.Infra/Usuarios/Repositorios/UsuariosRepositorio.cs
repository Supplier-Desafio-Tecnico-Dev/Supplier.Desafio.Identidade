using Dapper;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;

namespace Supplier.Desafio.Identidade.Infra.Usuarios.Repositorios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly DatabaseConnection _databaseConnection;

        public UsuariosRepositorio(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<int> InserirAsync(Usuario usuario)
        {
            using (var connection = _databaseConnection.CreateConnection())
            {
                var query = @"
                INSERT INTO usuarios (Email, Senha, SenhaSalt)
                VALUES (@Email, @Senha, @SenhaSalt);
                SELECT LAST_INSERT_ID();
                ";

                int id = await connection.ExecuteScalarAsync<int>(query, new
                {
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    SenhaSalt = usuario.SenhaSalt
                });

                return id;
            }
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            using (var connection = _databaseConnection.CreateConnection())
            {
                var query = "SELECT * FROM usuarios WHERE Email = @Email";
                return await connection.QuerySingleOrDefaultAsync<Usuario>(query, new { Email = email });
            }
        }
    }
}
