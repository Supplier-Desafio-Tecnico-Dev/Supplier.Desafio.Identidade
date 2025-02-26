using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;

namespace Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;

public interface IUsuariosAppServico
{
    Task<UsuarioNovoResponse> InserirAsync(UsuarioNovoRequest request);
}