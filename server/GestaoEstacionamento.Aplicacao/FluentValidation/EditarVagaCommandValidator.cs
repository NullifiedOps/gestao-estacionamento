using FluentValidation;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

namespace GestaoEstacionamento.Core.Aplicacao.FluentValidation;

public class EditarVagaCommandValidator : AbstractValidator<EditarVagaCommand>
{
    public EditarVagaCommandValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty().WithMessage("O identificador é obrigatório.")
            .MinimumLength(2).WithMessage("O identificador deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(20).WithMessage("O identificador deve conter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Zona)
            .NotEmpty().WithMessage("A zona é obrigatória.")
            .MinimumLength(2).WithMessage("A zona deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(50).WithMessage("A zona deve conter no máximo {MaxLength} caracteres.");
    }
}
