using FluentValidation;

namespace Supplier.Desafio.Identidade.DataTransfer.Usuarios.Requests.Validadores;

public class UsuarioAutenticarRequestValidator : AbstractValidator<UsuarioAutenticarRequest>
{
    public UsuarioAutenticarRequestValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(c => c.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.");
    }
}