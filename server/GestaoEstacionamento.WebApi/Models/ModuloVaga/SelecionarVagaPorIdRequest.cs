namespace GestaoEstacionamento.WebApi.Models.ModuloVaga;

public record SelecionarVagaPorIdRequest(Guid Id);

public record SelecionarVagaPorIdResponse(
    Guid Id,
    string Zona,
    string Identificador,
    string? PlacaVeiculo,
    bool Ocupada
);
