namespace GestaoEstacionamento.WebApi.Models.ModuloVeiculo;

public record CadastrarVeiculoRequest(
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone
);

public record CadastrarVeiculoResponse(Guid Id, int numeroTicket);