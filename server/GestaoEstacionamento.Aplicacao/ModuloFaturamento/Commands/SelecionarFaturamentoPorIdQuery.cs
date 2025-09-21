using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;

public record SelecionarFaturamentoPorIdQuery(Guid Id) : IRequest<Result<SelecionarFaturamentoPorIdResult>>;

public record SelecionarFaturamentoPorIdResult(
    Guid Id,
    string PlacaVeiculo,
    decimal Valor,
    DateTime DataFaturamento
);