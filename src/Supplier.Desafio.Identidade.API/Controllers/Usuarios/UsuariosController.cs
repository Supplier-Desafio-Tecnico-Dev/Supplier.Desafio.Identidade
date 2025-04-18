﻿using Microsoft.AspNetCore.Mvc;
using Supplier.Desafio.Commons.Notificacoes;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests;
using Supplier.Desafio.Identidade.DataTransfer.Usuarios.Responses;

namespace Supplier.Desafio.Identidade.API.Controllers.Usuarios;

[Route("api/usuarios")]
public class UsuariosController : MainController
{
    private readonly IUsuariosAppServico _usuariosAppServico;

    public UsuariosController(IUsuariosAppServico usuariosAppServico, INotificador notificador) : base(notificador)
    {
        _usuariosAppServico = usuariosAppServico;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioNovoResponse>> InserirAsync(UsuarioNovoRequest request)
    {
        var response = await _usuariosAppServico.InserirAsync(request);

        return CustomResponse(response);
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult<UsuarioAutenticarResponse>> AutenticarAsync(UsuarioAutenticarRequest request)
    {
        var response = await _usuariosAppServico.AutenticarAsync(request);

        return CustomResponse(response);
    }
}
