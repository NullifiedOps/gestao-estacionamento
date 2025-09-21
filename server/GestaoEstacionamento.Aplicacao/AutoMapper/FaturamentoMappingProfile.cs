using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloFaturamento;
using System.Collections.Immutable;

namespace GestaoEstacionamento.Core.Aplicacao.AutoMapper;

public class FaturamentoMappingProfile : Profile
{
    public FaturamentoMappingProfile()
    {
        CreateMap<(FinalizarFaturamentoCommand command, decimal valor), Faturamento>()
            .ConvertUsing(src => new Faturamento(
                src.Item1.PlacaVeiculo,
                src.Item2
                ));

        CreateMap<Faturamento, FinalizarFaturamentoResult>()
            .ConvertUsing(src => new FinalizarFaturamentoResult(src.Valor));

        CreateMap<Faturamento, SelecionarFaturamentosDto>()
            .ConvertUsing(src => new SelecionarFaturamentosDto(
                src.Id,
                src.PlacaVeiculo,
                src.Valor,
                src.DataFaturamento
            ));

        CreateMap<Faturamento, SelecionarFaturamentoPorIdResult>()
            .ConvertUsing(src => new SelecionarFaturamentoPorIdResult(
                src.Id,
                src.PlacaVeiculo,
                src.Valor,
                src.DataFaturamento
            ));

        CreateMap<IEnumerable<Faturamento>, SelecionarFaturamentosResult>()
            .ConvertUsing((src, dest, ctx) =>
            new SelecionarFaturamentosResult(
                src?.Select(
                    c => ctx.Mapper.Map<SelecionarFaturamentosDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarFaturamentosDto>.Empty
            ));
    }
}
