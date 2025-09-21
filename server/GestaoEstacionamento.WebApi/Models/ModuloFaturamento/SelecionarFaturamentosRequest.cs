using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using System.Collections.Immutable;

namespace GestaoEstacionamento.WebApi.Models.ModuloFaturamento;

public record SelecionarFaturamentosRequest(int? quantidade);

public record SelecionarFaturamentosResponse(
    int Quantidade,
    ImmutableList<SelecionarFaturamentosDto> Faturamentos
);