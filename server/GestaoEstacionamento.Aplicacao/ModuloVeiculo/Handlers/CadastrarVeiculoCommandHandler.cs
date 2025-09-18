using AutoMapper;
using FluentResults;
using FluentValidation;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Handlers;

public class CadastrarVeiculoCommandHandler(
    IRepositorioVeiculo repositorioVeiculo,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<CadastrarVeiculoCommand> validator,
    ILogger<CadastrarVeiculoCommandHandler> logger
) : IRequestHandler<CadastrarVeiculoCommand, Result<CadastrarVeiculoResult>>
{
    public async Task<Result<CadastrarVeiculoResult>> Handle(
        CadastrarVeiculoCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

            return Result.Fail(erroFormatado);
        }

        var registros = await repositorioVeiculo.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Placa.Equals(command.Placa)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um carro registrado com esta placa."));

        try
        {
            var ultimoTicket = registros.Where(v => v.Ticket != null)
                .Select(v => v.Ticket.Numero).DefaultIfEmpty(0).Max();

            var novoTicket = ultimoTicket + 1;

            var veiculo = mapper.Map<Veiculo>((command, novoTicket));

            veiculo.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();
            veiculo.Ticket.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

            await repositorioVeiculo.CadastrarAsync(veiculo);

            await unitOfWork.CommitAsync();

            // Invalida o cache
            var cacheKey = $"veiculos:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = mapper.Map<CadastrarVeiculoResult>(veiculo);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante o registro de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
