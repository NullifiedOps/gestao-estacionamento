using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record DesocuparVagaCommand(
    string Identificador
) : IRequest<Result<DesocuparVagaResult>>;

public record DesocuparVagaResult();
