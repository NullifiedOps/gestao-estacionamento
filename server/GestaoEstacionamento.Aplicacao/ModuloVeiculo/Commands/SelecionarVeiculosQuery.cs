using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record SelecionarVeiculosQuery(int? Quantidade)
    : IRequest<Result<SelecionarVeiculosResult>>;

public record SelecionarVeiculosResult(ImmutableList<SelecionarVeiculosDto> Veiculos);

public record SelecionarVeiculosDto(
    Guid Id,
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone,
    int NumeroTicket
);
