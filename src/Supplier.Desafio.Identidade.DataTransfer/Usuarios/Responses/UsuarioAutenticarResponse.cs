namespace Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;

public record UsuarioAutenticarResponse(string Token,
                                        string Status,
                                        IReadOnlyList<string> DetalheErro);
