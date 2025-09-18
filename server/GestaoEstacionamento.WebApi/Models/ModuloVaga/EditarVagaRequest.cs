namespace GestaoEstacionamento.WebApi.Models.ModuloVaga;

public record EditarVagaRequest(
    string Zona,
    string Identificador
);

public record EditarVagaResponse(
    string Zona,
    string Identificador
);