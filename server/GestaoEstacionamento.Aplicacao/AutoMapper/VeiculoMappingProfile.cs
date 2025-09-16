using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using System.Collections.Immutable;

namespace GestaoEstacionamento.Core.Aplicacao.AutoMapper;

public class VeiculoMappingProfile : Profile
{
    public VeiculoMappingProfile()
    {
        CreateMap<(CadastrarVeiculoCommand command, int numeroTicket), Veiculo>()
            .ConvertUsing(src => new Veiculo(
                src.Item1.Placa,
                src.Item1.Modelo,
                src.Item1.Cor,
                src.Item1.Detalhes,
                src.Item1.Nome,
                src.Item1.Cpf,
                src.Item1.Telefone,
                src.numeroTicket
                ));

        CreateMap<Veiculo, CadastrarVeiculoResult>()
            .ConstructUsing(src => new CadastrarVeiculoResult(src.Id, src.Ticket.Numero));

        CreateMap<EditarVeiculoCommand, Veiculo>();
        CreateMap<Veiculo, EditarVeiculoResult>();

        CreateMap<Veiculo, SelecionarVeiculoPorIdResult>()
            .ConvertUsing(src => new SelecionarVeiculoPorIdResult(
                src.Id,
                src.Placa,
                src.Modelo,
                src.Cor,
                src.Detalhes,
                src.Nome,
                src.Cpf,
                src.Telefone,
                src.Ticket.Numero
            ));

        CreateMap<Veiculo, SelecionarVeiculosDto>()
            .ConvertUsing(src => new SelecionarVeiculosDto(
                src.Id,
                src.Placa,
                src.Modelo,
                src.Cor,
                src.Detalhes,
                src.Nome,
                src.Cpf,
                src.Telefone,
                src.Ticket.Numero
            ));

        CreateMap<IEnumerable<Veiculo>, SelecionarVeiculosResult>()
            .ConvertUsing((src, dest, ctx) => 
            new SelecionarVeiculosResult(
                src?.Select(c => ctx.Mapper.Map<SelecionarVeiculosDto>(c))
                    .ToImmutableList() ?? ImmutableList<SelecionarVeiculosDto>.Empty
                )
            );
    }
}
