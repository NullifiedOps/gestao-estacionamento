using AutoMapper;
using FluentResults;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloFaturamento;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Handlers;

internal class SelecionarFaturamentoPorIdQueryHandler(
    IMapper mapper,
    IRepositorioFaturamento repositorioFaturamentos,
    ILogger<SelecionarFaturamentoPorIdQueryHandler> logger
) : IRequestHandler<SelecionarFaturamentoPorIdQuery, Result<SelecionarFaturamentoPorIdResult>>
{
    public async Task<Result<SelecionarFaturamentoPorIdResult>> Handle(SelecionarFaturamentoPorIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registro = await repositorioFaturamentos.SelecionarRegistroPorIdAsync(query.Id);

            if (registro is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(query.Id));

            var result = mapper.Map<SelecionarFaturamentoPorIdResult>(registro);

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
