using Supplier.Desafio.Identidade.Aplicacao.Core;
using Supplier.Desafio.Identidade.Aplicacao.Core.Notificacoes;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests.Validadores;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;
using Supplier.Desafio.Identidade.Dominio.Core;
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
    
    public async Task<UsuarioAutenticarResponse> AutenticarAsync(UsuarioAutenticarRequest request)
    {
        if (!ExecutarValidacao(new UsuarioAutenticarRequestValidator(), request))
            return new UsuarioAutenticarResponse { };
        
        // Buscar usuário no banco de dados
        var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);
        if (usuario == null)
            return new UsuarioAutenticarResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." };

        // Validar senha
        var senha = CryptoHelper.Decrypt(usuario.Senha, "");
        if (senha != request.Senha)
            return new UsuarioAutenticarResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." };

        // Gerar token JWT
        var token = _jwtService.GerarToken(usuario);
        
        return new UsuarioAutenticarResponse { Token = token };
    }
}