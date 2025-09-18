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

public class EditarVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<EditarVagaCommand> validator,
    ILogger<EditarVagaCommandHandler> logger
) : IRequestHandler<EditarVagaCommand, Result<EditarVagaResult>>
{
    public async Task<Result<EditarVagaResult>> Handle(EditarVagaCommand command, CancellationToken cancellationToken)
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
            var vagaEditado = mapper.Map<Vaga>(command);

            await repositorioVaga.EditarAsync(command.Id, vagaEditado);

            await unitOfWork.CommitAsync();

            // Invalida o cache
            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = mapper.Map<EditarVagaResult>(vagaEditado);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante a edição de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}

