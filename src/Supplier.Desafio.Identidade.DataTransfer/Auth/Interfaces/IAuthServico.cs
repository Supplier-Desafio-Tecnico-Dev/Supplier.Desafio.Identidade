namespace Supplier.Desafio.Identidade.Dominio.Auth.Servicos
{
    public interface IAuthServico
    {
        string GerarToken(string email);
    }
}