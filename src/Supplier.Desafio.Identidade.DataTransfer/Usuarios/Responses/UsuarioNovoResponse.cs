namespace Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;

public class UsuarioNovoResponse
{
    public int Id { get; set; }
    public string Status { get; set; }
    public IReadOnlyList<string> DetalheErro { get; set; }
}