using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record SelecionarVeiculoPorIdQuery(Guid Id) : IRequest<Result<SelecionarVeiculoPorIdResult>>;

public record SelecionarVeiculoPorIdResult(
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
