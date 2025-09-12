namespace GestaoEstacionamento.WebApi.Models.ModuloVeiculo;

public record EditarVeiculoRequest(
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone
);

public record EditarVeiculoResponse(
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone
);

