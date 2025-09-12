using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record ExcluirVeiculoCommand(Guid Id) : IRequest<Result<ExcluirVeiculoResult>>;

public record ExcluirVeiculoResult();
