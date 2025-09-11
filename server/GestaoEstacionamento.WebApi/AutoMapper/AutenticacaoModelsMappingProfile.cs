using AutoMapper;
using GestaoEstacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;
using GestaoEstacionamento.WebApi.Models.ModuloAutenticacao;

namespace GestaoEstacionamento.WebApi.AutoMapper;

public class AutenticacaoModelsMappingProfile : Profile
{
    public AutenticacaoModelsMappingProfile()
    {
        CreateMap<RegistrarUsuarioRequest, RegistrarUsuarioCommand>();
        CreateMap<AutenticarUsuarioRequest, AutenticarUsuarioCommand>();
    }
}
