using FluentResults;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class DesocuparVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IDistributedCache cache,
    ILogger<DesocuparVagaCommand> logger
    ) : IRequestHandler<DesocuparVagaCommand, Result<DesocuparVagaResult>>
{
    public async Task<Result<DesocuparVagaResult>> Handle(DesocuparVagaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var vagas = await repositorioVaga.SelecionarRegistrosAsync();
            var vagaSelecionada = vagas.Find(v => v.Identificador.Equals(command.Identificador) && v.UsuarioId == tenantProvider.UsuarioId);

            if (vagaSelecionada is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro($"Vaga não encontrada com o identificador: {command.Identificador}"));

            if (!vagaSelecionada.Ocupada)
                return Result.Fail(ResultadosErro.RequisicaoInvalidaErro("Não foi possivel remover um veiculo da vaga pois ela não contém um."));

            vagaSelecionada.RemoverVeiculo();

            await unitOfWork.CommitAsync();

            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = new DesocuparVagaResult();

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