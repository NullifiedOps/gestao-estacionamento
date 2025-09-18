using AutoMapper;
using FluentResults;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class SelecionarVagaPorIdQueryHandler(
    IMapper mapper,
    IRepositorioVaga repositorioVaga,
    ILogger<SelecionarVagaPorIdQueryHandler> logger
) : IRequestHandler<SelecionarVagaPorIdQuery, Result<SelecionarVagaPorIdResult>>
{
    public async Task<Result<SelecionarVagaPorIdResult>> Handle(SelecionarVagaPorIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registro = await repositorioVaga.SelecionarRegistroPorIdAsync(query.Id);

            if (registro is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(query.Id));

            var result = mapper.Map<SelecionarVagaPorIdResult>(registro);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro durante a seleção de {@Registro}.",
                query
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}

