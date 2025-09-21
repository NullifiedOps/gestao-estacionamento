namespace GestaoEstacionamento.WebApi.Models.ModuloVeiculo;

public record SelecionarVeiculoPorIdRequest(Guid Id);

public record SelecionarVeiculoPorIdResponse(
    Guid Id,
    string Placa,
    string Modelo,
    string Cor,
    string? Detalhes,
    string Nome,
    string Cpf,
    string Telefone,
    int NumeroTicket,
    bool TicketEncerrado
);
