using AutoMapper;
using FluentResults;
using FluentValidation;
using GestaoEstacionamento.Core.Aplicacao.Compartilhado;
using GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Commands;
using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class CadastrarVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<CadastrarVagaCommand> validator,
    ILogger<CadastrarVagaCommandHandler> logger
) : IRequestHandler<CadastrarVagaCommand, Result<CadastrarVagaResult>>
{
    public async Task<Result<CadastrarVagaResult>> Handle(
        CadastrarVagaCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

            return Result.Fail(erroFormatado);
        }

        var registros = await repositorioVaga.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Identificador.Equals(command.Identificador)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe uma vaga registrada com este identificador."));

        try
        {
            var vaga = mapper.Map<Vaga>(command);

            vaga.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

            await repositorioVaga.CadastrarAsync(vaga);

            await unitOfWork.CommitAsync();

            // Invalida o cache
            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = mapper.Map<CadastrarVagaResult>(vaga);

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
