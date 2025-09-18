using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using System.Collections.Immutable;

namespace GestaoEstacionamento.WebApi.Models.ModuloVaga;

public record SelecionarVagasRequest(int? quantidade);

public record SelecionarVagasResponse(
    int Quantidade,
    ImmutableList<SelecionarVagasDto> Vagas
);
