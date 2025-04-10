﻿using FluentValidation;

namespace Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests.Validadores
{
    public class UsuarioNovoRequestValidador : AbstractValidator<UsuarioNovoRequest>
    {
        public UsuarioNovoRequestValidador()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
