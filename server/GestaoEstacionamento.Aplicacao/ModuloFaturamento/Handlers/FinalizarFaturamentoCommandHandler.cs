using AutoMapper;
using FluentResults;
using FluentValidation;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Handlers;

public class FinalizarFaturamentoCommandHandler(
    IRepositorioFaturamento repositorioFaturamento,
    IRepositorioVeiculo repositorioVeiculo,
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<FinalizarFaturamentoCommand> validator,
    ILogger<FinalizarFaturamentoCommandHandler> logger
) : IRequestHandler<FinalizarFaturamentoCommand, Result<FinalizarFaturamentoResult>>
{
    public async Task<Result<FinalizarFaturamentoResult>> Handle(FinalizarFaturamentoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

            if (!resultadoValidacao.IsValid)
            {
                var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

                var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

                return Result.Fail(erroFormatado);
            }

            var veiculos = await repositorioVeiculo.SelecionarRegistrosAsync();
            var veiculoSelecionado = veiculos.Find(v => v.Placa.Equals(command.PlacaVeiculo) && v.UsuarioId == tenantProvider.UsuarioId);

            if (veiculoSelecionado is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro($"Veículo não encontrado com a placa: {command.PlacaVeiculo}"));

            if (veiculoSelecionado.Ticket.Encerrado)
                return Result.Fail(ResultadosErro.RequisicaoInvalidaErro("O veículo possui ticket já encerrado."));

            var vagas = await repositorioVaga.SelecionarRegistrosAsync();
            var vagaOcupada = vagas.FirstOrDefault(v => v.Veiculo != null && v.Veiculo.Id == veiculoSelecionado.Id);

            if (vagaOcupada is not null)
                return Result.Fail(ResultadosErro.RequisicaoInvalidaErro($"O veículo com placa {command.PlacaVeiculo} ainda está estacionado na vaga {vagaOcupada.Identificador}."));

            veiculoSelecionado.Ticket.DataSaida = DateTime.UtcNow;

            var diasEstacionados = Math.Max(1, (veiculoSelecionado.Ticket.DataSaida.Value - veiculoSelecionado.Ticket.DataEntrada).Days);
            var valorTotal = diasEstacionados * command.ValorDiaria;

            var faturamento = mapper.Map<Faturamento>((command, valorTotal));

            faturamento.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

            await repositorioFaturamento.CadastrarAsync(faturamento);

            var result = mapper.Map<FinalizarFaturamentoResult>(faturamento);

            await unitOfWork.CommitAsync();

            var cacheKey = $"faturamento:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

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
