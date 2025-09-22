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

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class EstacionarVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    IRepositorioVeiculo repositorioVeiculo,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IDistributedCache cache,
    ILogger<EstacionarVagaCommand> logger
    ) : IRequestHandler<EstacionarVagaCommand, Result<EstacionarVagaResult>>
{
    public async Task<Result<EstacionarVagaResult>> Handle(EstacionarVagaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var vagas = await repositorioVaga.SelecionarRegistrosAsync();
            var vagaSelecionada = vagas.Find(v => v.Identificador.Equals(command.Identificador) && v.UsuarioId == tenantProvider.UsuarioId);
            
            var veiculos = await repositorioVeiculo.SelecionarRegistrosAsync();
            var veiculoSelecionado = veiculos.Find(v => v.Placa.Equals(command.PlacaVeiculo) && v.UsuarioId == tenantProvider.UsuarioId);

            if (vagaSelecionada is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro($"Vaga não encontrada com o identificador: {command.Identificador}"));

            if (vagaSelecionada.Ocupada)
                return Result.Fail(ResultadosErro.RequisicaoInvalidaErro("Não foi possivel estacionar na vaga pois já contém um veiculo."));

            if (veiculoSelecionado is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro($"Veículo não encontrado com a placa: {command.PlacaVeiculo}"));

            var vagaOcupadaPeloVeiculo = vagas.FirstOrDefault(v => v.Veiculo != null && v.Veiculo.Id == veiculoSelecionado.Id);

            if (vagaOcupadaPeloVeiculo is not null)
                return Result.Fail(ResultadosErro.RequisicaoInvalidaErro($"O veículo com placa {command.PlacaVeiculo} já está estacionado na vaga {vagaOcupadaPeloVeiculo.Identificador}."));

            if (veiculoSelecionado.Ticket.Encerrado)
                return Result.Fail(ResultadosErro.RequisicaoInvalidaErro($"O veículo não possui um ticket ativo."));

            vagaSelecionada.EstacionarVeiculo(veiculoSelecionado);

            await unitOfWork.CommitAsync();

            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = new EstacionarVagaResult();

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
