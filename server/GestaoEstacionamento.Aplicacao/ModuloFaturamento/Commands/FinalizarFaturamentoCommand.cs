using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;

public record FinalizarFaturamentoCommand(
    string PlacaVeiculo,
    decimal ValorDiaria
) : IRequest<Result<FinalizarFaturamentoResult>>;

public record FinalizarFaturamentoResult(decimal ValorFinal);