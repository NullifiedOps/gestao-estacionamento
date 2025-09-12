using FluentValidation;
using GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

namespace GestaoEstacionamento.Core.Aplicacao.FluentValidation;

public class EditarVeiculoCommandValidator : AbstractValidator<EditarVeiculoCommand>
{
    public EditarVeiculoCommandValidator()
    {
        RuleFor(x => x.Placa)
           .NotEmpty().WithMessage("A placa é obrigatória.")
           .Matches(@"^(?:[A-Z]{3}-\d{4}|[A-Z]{3}\d[A-Z]\d{2})$").WithMessage("A placa deve seguir o formato AAA-0000 ou BRA0S17.");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O modelo é obrigatório.")
            .MinimumLength(2).WithMessage("O modelo deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(50).WithMessage("O modelo deve conter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Cor)
            .NotEmpty().WithMessage("A cor é obrigatória.")
            .MinimumLength(2).WithMessage("A cor deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(50).WithMessage("A cor deve conter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Detalhes)
            .MaximumLength(200).WithMessage("Os detalhes devem conter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do proprietário é obrigatório.")
            .MinimumLength(2).WithMessage("O nome do proprietário deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O nome do proprietário deve conter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$").WithMessage("O CPF deve seguir o formato 000.000.000-00 ou 00000000000.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage("O telefone deve seguir o formato (00) 00000-0000, (00) 0000-0000.");
    }
}

