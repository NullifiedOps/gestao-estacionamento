using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using System.Collections.Immutable;

namespace GestaoEstacionamento.Core.Aplicacao.AutoMapper;

public class VagaMappingProfile : Profile
{
    public VagaMappingProfile()
    {
        CreateMap<CadastrarVagaCommand, Vaga>();
        CreateMap<Vaga, CadastrarVagaResult>();

        CreateMap<EditarVagaCommand, Vaga>();
        CreateMap<Vaga, EditarVagaResult>();

        CreateMap<Vaga, SelecionarVagaPorIdResult>()
            .ConvertUsing(src => new SelecionarVagaPorIdResult(
                src.Id,
                src.Zona,
                src.Identificador,
                src.Ocupada ? src.Veiculo!.Placa : "Nenhum Veículo.",
                src.Ocupada
            ));

        CreateMap<Vaga, SelecionarVagasDto>()
            .ConvertUsing(src => new SelecionarVagasDto(
                src.Id,
                src.Zona,
                src.Identificador,
                src.Ocupada ? src.Veiculo!.Placa : "Nenhum Veículo.",
                src.Ocupada
            ));

        CreateMap<IEnumerable<Vaga>, SelecionarVagasResult>()
            .ConvertUsing((src, dest, ctx) =>
            new SelecionarVagasResult(
                src?.Select(c => ctx.Mapper.Map<SelecionarVagasDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarVagasDto>.Empty
                )
            );
    }
}
