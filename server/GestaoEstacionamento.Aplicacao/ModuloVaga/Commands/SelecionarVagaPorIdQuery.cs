using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record SelecionarVagaPorIdQuery(Guid Id) : IRequest<Result<SelecionarVagaPorIdResult>>;

public record SelecionarVagaPorIdResult(
    Guid Id, 
    string Zona, 
    string Identificador, 
    string? PlacaVeiculo,
    bool Ocupada
);