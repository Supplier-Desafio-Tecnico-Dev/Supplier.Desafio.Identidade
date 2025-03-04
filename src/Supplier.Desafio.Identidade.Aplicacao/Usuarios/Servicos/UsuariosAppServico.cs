using Supplier.Desafio.Commons;
using Supplier.Desafio.Commons.Auth;
using Supplier.Desafio.Commons.Enums;
using Supplier.Desafio.Commons.Helpers;
using Supplier.Desafio.Commons.Notificacoes;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests.Validadores;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;

namespace Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos;

public class UsuariosAppServico : ServicoBase, IUsuariosAppServico
{
    private readonly IUsuariosRepositorio _usuariosRepositorio;
    private readonly IAuthServico _authServico;
    private readonly INotificador _notificador;

    public UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio,
                              INotificador notificador,
                              IAuthServico authServico) : base(notificador)
    {
        _usuariosRepositorio = usuariosRepositorio;
        _notificador = notificador;
        _authServico = authServico;
    }

    public async Task<UsuarioNovoResponse> InserirAsync(UsuarioNovoRequest request)
    {
        if (!ExecutarValidacao(new UsuarioNovoRequestValidador(), request))
            return new UsuarioNovoResponse(0, ProcessamentoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());

        var usuarioExistente = await _usuariosRepositorio.ObterPorEmailAsync(request.Email);
        if (usuarioExistente != null)
        {
            Notificar("Esse e-mail já está sendo utilizado");
            return new UsuarioNovoResponse(0, ProcessamentoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());
        }

        var usuario = new Usuario(request.Email, request.Senha);

        var id = await _usuariosRepositorio.InserirAsync(usuario);
        if (id <= 0)
        {
            Notificar("Erro ao inserir cliente");
            return new UsuarioNovoResponse(0, ProcessamentoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());
        }

        return new UsuarioNovoResponse(id, ProcessamentoEnum.Ok.ToString(), []);
    }

    public async Task<UsuarioAutenticarResponse> AutenticarAsync(UsuarioAutenticarRequest request)
    {
        if (!ExecutarValidacao(new UsuarioAutenticarRequestValidador(), request))
            return new UsuarioAutenticarResponse(string.Empty, ProcessamentoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());

        var usuario = await _usuariosRepositorio.ObterPorEmailAsync(request.Email);

        if (usuario == null)
        {
            Notificar("Usuário ou senha inválido.");
            return new UsuarioAutenticarResponse(string.Empty, ProcessamentoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());
        }

        var senha = CryptoHelper.Decrypt(usuario.Senha, usuario.SenhaSalt);

        if (senha != request.Senha)
        {
            Notificar("Usuário ou senha inválido.");
            return new UsuarioAutenticarResponse(string.Empty, ProcessamentoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());
        }

        var token = _authServico.GerarToken(usuario.Email);

        return new UsuarioAutenticarResponse(token, ProcessamentoEnum.Ok.ToString(), []);
    }
}
