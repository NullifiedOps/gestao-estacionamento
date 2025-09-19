namespace GestaoEstacionamento.WebApi.Models.ModuloVaga;

public record EstacionarVagaRequest(
    string Identificador,
    string PlacaVeiculo
);

public record EstacionarVagaResponse();
