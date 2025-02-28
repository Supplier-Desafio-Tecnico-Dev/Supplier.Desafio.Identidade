namespace Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;

public class UsuarioAutenticarResponse
{
    public string Token { get; set; }
    public string Status { get; set; }
    public IReadOnlyList<string> DetalheErro { get; set; }
}