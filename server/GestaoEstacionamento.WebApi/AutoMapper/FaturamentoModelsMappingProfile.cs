using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.WebApi.Models.ModuloFaturamento;
using System.Collections.Immutable;

namespace GestaoEstacionamento.WebApi.AutoMapper;

public class FaturamentoModelsMappingProfile : Profile
{
    public FaturamentoModelsMappingProfile()
    {
        CreateMap<FinalizarFaturamentoRequest, FinalizarFaturamentoCommand>()
            .ConvertUsing(src => new FinalizarFaturamentoCommand(
                src.PlacaVeiculo, 
                src.ValorDiaria
                ));

        CreateMap<FinalizarFaturamentoResult, FinalizarFaturamentoResponse>()
            .ConvertUsing(src => new FinalizarFaturamentoResponse(
                src.ValorFinal
                ));

        CreateMap<SelecionarFaturamentosRequest, SelecionarFaturamentosQuery>();

        CreateMap<SelecionarFaturamentosResult, SelecionarFaturamentosResponse>()
            .ConvertUsing((src, dest, ctx) => new SelecionarFaturamentosResponse(
                src.Faturamentos.Count,
                src?.Faturamentos.Select(
                    c => ctx.Mapper.Map<SelecionarFaturamentosDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarFaturamentosDto>.Empty
                ));

        CreateMap<Guid, SelecionarFaturamentoPorIdQuery>()
            .ConvertUsing(src => new SelecionarFaturamentoPorIdQuery(src));

        CreateMap<SelecionarFaturamentoPorIdResult, SelecionarFaturamentoPorIdResponse>()
            .ConvertUsing(src => new SelecionarFaturamentoPorIdResponse(
                src.Id,
                src.PlacaVeiculo,
                src.Valor,
                src.DataFaturamento
                ));
    }
}
