namespace Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;

public record UsuarioNovoResponse(int Id, string Status, IReadOnlyList<string> DetalheErro);
