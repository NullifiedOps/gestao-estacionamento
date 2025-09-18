using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record SelecionarVagasQuery(int? Quantidade)
    : IRequest<Result<SelecionarVagasResult>>;

public record SelecionarVagasResult(ImmutableList<SelecionarVagasDto> Vagas);

public record SelecionarVagasDto(
    Guid Id,
    string Zona,
    string Identificador,
    string? PlacaVeiculo,
    bool Ocupada
);

