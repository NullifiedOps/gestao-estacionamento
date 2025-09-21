namespace GestaoEstacionamento.WebApi.Models.ModuloFaturamento;

public record SelecionarFaturamentoPorIdRequest(Guid Id);

public record SelecionarFaturamentoPorIdResponse(
    Guid Id,
    string PlacaVeiculo,
    decimal Valor,
    DateTime DataFaturamento
);
