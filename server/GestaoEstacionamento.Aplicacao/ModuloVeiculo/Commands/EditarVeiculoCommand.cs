using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record EditarVeiculoCommand(
    Guid Id,
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone
) : IRequest<Result<EditarVeiculoResult>>;

public record EditarVeiculoResult(
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone
);