using Supplier.Desafio.Identidade.Aplicacao.Core;
using Supplier.Desafio.Identidade.Aplicacao.Core.Notificacoes;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests.Validadores;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;
using Supplier.Desafio.Identidade.Dominio.Auth.Servicos;
using Supplier.Desafio.Identidade.Dominio.Core;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;

namespace Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos;

public class UsuariosAppServico : ServicoBase, IUsuariosAppServico
{
    private readonly IUsuariosRepositorio _usuarioRepositorio;
    private readonly IAuthServico _authServico;
    private readonly INotificador _notificador;

    public UsuariosAppServico(INotificador notificador,
                              IUsuariosRepositorio usuarioRepositorio,
                              IAuthServico authServico) : base(notificador)
    {
        _notificador = notificador;
        _usuarioRepositorio = usuarioRepositorio;
        _authServico = authServico;
    }

    public async Task<UsuarioNovoResponse> InserirAsync(UsuarioNovoRequest request)
    {
        if (!ExecutarValidacao(new UsuarioNovoRequestValidator(), request))
            return new UsuarioNovoResponse { Status = "ERRO", DetalheErro = _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList() };

        var usuarioExistente = await _usuarioRepositorio.ObterPorEmailAsync(request.Email);
        if (usuarioExistente != null)
        {
            Notificar("Esse e-mail já está sendo utilizado");
            return new UsuarioNovoResponse { Status = "ERRO", DetalheErro = _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList() };
        }
        
        var usuario = new Usuario(request.Email, request.Senha);

        var id = await _usuarioRepositorio.InserirAsync(usuario);
        if (id <= 0)
        {
            Notificar("Erro ao inserir cliente");
            return new UsuarioNovoResponse { Status = "ERRO", DetalheErro = _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList() };
        }

        return new UsuarioNovoResponse { Id = id };
    }

    public async Task<UsuarioAutenticarResponse> AutenticarAsync(UsuarioAutenticarRequest request)
    {
        if (!ExecutarValidacao(new UsuarioAutenticarRequestValidator(), request))
            return new UsuarioAutenticarResponse { };

        var usuario = await _usuarioRepositorio.ObterPorEmailAsync(request.Email);
        if (usuario == null)
        {
            Notificar("Usuário ou senha inválido.");
            return new UsuarioAutenticarResponse { Status = "ERRO", DetalheErro = _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList() };
        }

        var senha = CryptoHelper.Decrypt(usuario.Senha, usuario.SenhaSalt);
        if (senha != request.Senha)
        {
            Notificar("Usuário ou senha inválido.");
            return new UsuarioAutenticarResponse { Status = "ERRO", DetalheErro = _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList() };
        }

        var token = _authServico.GerarToken(usuario.Email);

        return new UsuarioAutenticarResponse { Token = token };
    }
}