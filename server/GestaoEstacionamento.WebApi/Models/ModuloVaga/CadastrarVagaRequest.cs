namespace GestaoEstacionamento.WebApi.Models.ModuloVaga;

public record CadastrarVagaRequest(
    string Zona,
    string Identificador
);

public record CadastrarVagaResponse(Guid Id);