using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoEstacionamento.WebApi.Models.ModuloVeiculo;
using System.Collections.Immutable;

namespace GestaoEstacionamento.WebApi.AutoMapper;

public class VeiculoModelsMappingProfile : Profile
{
    public VeiculoModelsMappingProfile()
    {
        CreateMap<CadastrarVeiculoRequest, CadastrarVeiculoCommand>();
        CreateMap<CadastrarVeiculoResult, CadastrarVeiculoResponse>();

        CreateMap<(Guid, EditarVeiculoRequest), EditarVeiculoCommand>()
            .ConvertUsing(src => new EditarVeiculoCommand(
                src.Item1,
                src.Item2.Placa,
                src.Item2.Modelo,
                src.Item2.Cor,
                src.Item2.Detalhes,
                src.Item2.Nome,
                src.Item2.Cpf,
                src.Item2.Telefone
            ));
        CreateMap<EditarVeiculoResult, EditarVeiculoResponse>();

        CreateMap<Guid, ExcluirVeiculoCommand>()
            .ConvertUsing(src => new ExcluirVeiculoCommand(src));

        CreateMap<SelecionarVeiculosRequest, SelecionarVeiculosQuery>();
        
        CreateMap<SelecionarVeiculosResult, SelecionarVeiculosResponse>()
            .ConvertUsing((src, dest, ctx) => new SelecionarVeiculosResponse(
                src.Veiculos.Count,
                src?.Veiculos.Select(
                    c => ctx.Mapper.Map<SelecionarVeiculosDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarVeiculosDto>.Empty
            ));

        CreateMap<Guid, SelecionarVeiculoPorIdQuery>()
            .ConvertUsing(src => new SelecionarVeiculoPorIdQuery(src));

        CreateMap<SelecionarVeiculoPorIdResult, SelecionarVeiculoPorIdResponse>()
            .ConvertUsing(src => new SelecionarVeiculoPorIdResponse(
                src.Id,
                src.Placa,
                src.Modelo,
                src.Cor,
                src.Detalhes,
                src.Nome,
                src.Cpf,
                src.Telefone,
                src.NumeroTicket
            ));
    }
}
