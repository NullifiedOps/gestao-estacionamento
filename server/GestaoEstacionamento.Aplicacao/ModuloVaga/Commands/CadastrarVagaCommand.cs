using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record CadastrarVagaCommand(
    string Zona,
    string Identificador
) : IRequest<Result<CadastrarVagaResult>>;

public record CadastrarVagaResult(Guid Id);
