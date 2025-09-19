using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record EstacionarVagaCommand(
    string Identificador,
    string PlacaVeiculo
) : IRequest<Result<EstacionarVagaResult>>;

public record EstacionarVagaResult();