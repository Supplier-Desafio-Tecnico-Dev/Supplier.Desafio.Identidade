using Dapper;
using MySqlX.XDevAPI;
using Supplier.Commons.Data;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;

namespace Supplier.Desafio.Identidade.Infra.Usuarios.Repositorios;

public class UsuariosRepositorio : RepositorioDapper<Usuario>, IUsuariosRepositorio
{
    public UsuariosRepositorio(DapperDbContext dapperContext) : base(dapperContext)
    {
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        var query = "SELECT * FROM usuarios WHERE Email = @Email";
        var parameters = new DynamicParameters();
        parameters.Add("@Email", email);

        return await session.QueryFirstOrDefaultAsync<Usuario>(query, parameters);
    }

    public async Task<int> InserirAsync(Usuario usuario)
    {
        var query = @"
                INSERT INTO usuarios (Email, Senha, SenhaSalt)
                VALUES (@Email, @Senha, @SenhaSalt);
                SELECT LAST_INSERT_ID();
                ";

        var parameters = new DynamicParameters();
        parameters.Add("@Email", usuario.Email);
        parameters.Add("@Senha", usuario.Senha);
        parameters.Add("@SenhaSalt", usuario.SenhaSalt);

        return await session.ExecuteScalarAsync<int>(query, parameters);
    }
}
