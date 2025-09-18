using AutoMapper;
using FluentResults;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class SelecionarVagasQueryHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<SelecionarVagasQueryHandler> logger
) : IRequestHandler<SelecionarVagasQuery, Result<SelecionarVagasResult>>
{
    public async Task<Result<SelecionarVagasResult>> Handle(SelecionarVagasQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cacheQuery = query.Quantidade.HasValue ? $"q={query.Quantidade.Value}" : "q=all";
            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:{cacheQuery}";

            // 1) Tenta acessar o cache
            var jsonString = await cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                var registrosEmCache = JsonSerializer.Deserialize<SelecionarVagasResult>(jsonString);

                if (registrosEmCache is not null)
                    return Result.Ok(registrosEmCache);
            }

            // 2) Cache miss -> busca no repositório
            var registros = query.Quantidade.HasValue ?
                await repositorioVaga.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioVaga.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarVagasResult>(registros);

            // 3) Salva os resultados novos no cache
            var jsonPayload = JsonSerializer.Serialize(result);

            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60) };

            await cache.SetStringAsync(cacheKey, jsonPayload, cacheOptions, cancellationToken);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro durante a seleção de {@Registros}.",
                query
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
