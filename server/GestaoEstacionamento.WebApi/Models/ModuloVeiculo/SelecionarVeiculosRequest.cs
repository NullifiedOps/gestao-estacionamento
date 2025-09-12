using GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using System.Collections.Immutable;

namespace GestaoEstacionamento.WebApi.Models.ModuloVeiculo;

public record SelecionarVeiculosRequest(int? quantidade);

public record SelecionarVeiculosResponse(
    int Quantidade,
    ImmutableList<SelecionarVeiculosDto> Veiculos
);
