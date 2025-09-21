namespace GestaoEstacionamento.WebApi.Models.ModuloFaturamento;

public record FinalizarFaturamentoRequest(
    string PlacaVeiculo,
    decimal ValorDiaria
);

public record FinalizarFaturamentoResponse(decimal ValorTotal);