using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;

namespace Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;

public interface IUsuariosRepositorio : IRepositorioDapper<Usuario>
{
    Task<int> InserirAsync(Usuario usuario);
    Task<Usuario?> ObterPorEmailAsync(string email);
}
