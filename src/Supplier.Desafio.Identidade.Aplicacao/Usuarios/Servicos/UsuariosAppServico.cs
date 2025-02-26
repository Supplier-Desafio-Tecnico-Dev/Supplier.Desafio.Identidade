using Supplier.Desafio.Identidade.Aplicacao.Core;
using Supplier.Desafio.Identidade.Aplicacao.Core.Notificacoes;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests.Validadores;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;

namespace Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos;

public class UsuariosAppServico : ServicoBase, IUsuariosAppServico
{
    private readonly INotificador _notificador;
    
    public UsuariosAppServico(INotificador notificador) : base(notificador)
    {
        _notificador = notificador;
    }
    
    public async Task<UsuarioNovoResponse> InserirAsync(UsuarioNovoRequest request)
    {
        if (!ExecutarValidacao(new UsuarioNovoRequestValidator(), request))
            return new UsuarioNovoResponse { };

        // Verificar se usuário já existe
        if (request.Email == "Igor")
        {
            Notificar("Usuário já cadastrado");
            return new UsuarioNovoResponse {  };
        }
        
        var usuario = new Usuario(request.Email, request.Senha);
        
        // Persistir cliente
        
        return new UsuarioNovoResponse { Id = usuario.Id };
    }
}