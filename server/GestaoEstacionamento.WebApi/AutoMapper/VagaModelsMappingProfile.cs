using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.WebApi.Models.ModuloVaga;
using System.Collections.Immutable;

namespace GestaoEstacionamento.WebApi.AutoMapper;

public class VagaModelsMappingProfile : Profile
{
    public VagaModelsMappingProfile()
    {
        CreateMap<CadastrarVagaRequest, CadastrarVagaCommand>();
        CreateMap<CadastrarVagaResult, CadastrarVagaResponse>();

        CreateMap<(Guid, EditarVagaRequest), EditarVagaCommand>()
            .ConvertUsing(src => new EditarVagaCommand(
                src.Item1,
                src.Item2.Zona,
                src.Item2.Identificador
            ));
        CreateMap<EditarVagaResult, EditarVagaResponse>();

        CreateMap<Guid, ExcluirVagaCommand>()
            .ConvertUsing(src => new ExcluirVagaCommand(src));

        CreateMap<SelecionarVagasRequest, SelecionarVagasQuery>();

        CreateMap<SelecionarVagasResult, SelecionarVagasResponse>()
            .ConvertUsing((src, dest, ctx) => new SelecionarVagasResponse(
                src.Vagas.Count,
                src?.Vagas.Select(
                    c => ctx.Mapper.Map<SelecionarVagasDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarVagasDto>.Empty
            ));

        CreateMap<Guid, SelecionarVagaPorIdQuery>()
            .ConvertUsing(src => new SelecionarVagaPorIdQuery(src));

        CreateMap<SelecionarVagaPorIdResult, SelecionarVagaPorIdResponse>()
            .ConvertUsing(src => new SelecionarVagaPorIdResponse(
                src.Id,
                src.Zona,
                src.Identificador,
                src.PlacaVeiculo,
                src.Ocupada
            ));
    }
}
