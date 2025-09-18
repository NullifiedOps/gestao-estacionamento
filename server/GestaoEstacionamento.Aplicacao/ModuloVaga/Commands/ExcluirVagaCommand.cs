using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record ExcluirVagaCommand(Guid Id) : IRequest<Result<ExcluirVagaResult>>;

public record ExcluirVagaResult();
