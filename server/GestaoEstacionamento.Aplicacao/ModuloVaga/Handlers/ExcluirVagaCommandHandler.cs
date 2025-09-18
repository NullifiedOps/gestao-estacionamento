using FluentResults;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class ExcluirVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IDistributedCache cache,
    ILogger<ExcluirVagaCommandHandler> logger
) : IRequestHandler<ExcluirVagaCommand, Result<ExcluirVagaResult>>
{
    public async Task<Result<ExcluirVagaResult>> Handle(ExcluirVagaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var vagaSelecionado = await repositorioVaga.SelecionarRegistroPorIdAsync(command.Id);

            if (vagaSelecionado is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(command.Id));

            if (!vagaSelecionado.Ocupada)
                return Result.Fail(ResultadosErro.ExclusaoBloqueadaErro("Não foi possivel excluir o vaga pois ainda contém um veiculo."));

            await repositorioVaga.ExcluirAsync(command.Id);

            await unitOfWork.CommitAsync();

            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = new ExcluirVagaResult();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante a exclusão de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}

