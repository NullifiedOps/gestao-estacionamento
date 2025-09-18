using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record EditarVagaCommand(
    Guid Id,
    string Zona,
    string Identificador
) : IRequest<Result<EditarVagaResult>>;

public record EditarVagaResult(
    string Zona,
    string Identificador
);

