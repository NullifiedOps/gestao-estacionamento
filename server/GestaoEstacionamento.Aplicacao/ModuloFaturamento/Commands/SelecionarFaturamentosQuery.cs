using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;

public record SelecionarFaturamentosQuery(int? Quantidade)
    : IRequest<Result<SelecionarFaturamentosResult>>;

public record SelecionarFaturamentosResult(ImmutableList<SelecionarFaturamentosDto> Faturamentos);

public record SelecionarFaturamentosDto(
    Guid Id,
    string PlacaVeiculo,
    decimal Valor,
    DateTime DataFaturamento
);
