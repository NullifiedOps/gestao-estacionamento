using FluentValidation;
using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;

namespace GestaoEstacionamento.Core.Aplicacao.FluentValidation;

public class FinalizarFaturamentoCommandValidator : AbstractValidator<FinalizarFaturamentoCommand>
{
    public FinalizarFaturamentoCommandValidator()
    {
        RuleFor(x => x.PlacaVeiculo)
          .NotEmpty().WithMessage("A placa do veículo é obrigatória.")
          .MaximumLength(8).WithMessage("A placa do veículo deve ter no máximo 8 caracteres.");

        RuleFor(x => x.ValorDiaria)
            .GreaterThan(0).WithMessage("O valor da diária deve ser maior que zero.");
    }
}
