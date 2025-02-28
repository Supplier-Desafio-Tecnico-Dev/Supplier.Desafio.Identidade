using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;

namespace Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task<int> InserirAsync(Usuario usuario);
        Task<Usuario?> ObterPorEmailAsync(string email);
    }
}