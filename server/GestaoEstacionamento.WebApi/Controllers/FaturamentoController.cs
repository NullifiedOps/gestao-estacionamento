using AutoMapper;
using FluentResults;
using GestaoEstacionamento.Core.Aplicacao.ModuloFaturamento.Commands;
using GestaoEstacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using GestaoEstacionamento.WebApi.Models.ModuloFaturamento;
using GestaoEstacionamento.WebApi.Models.ModuloVeiculo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoEstacionamento.WebApi.Controllers;


[ApiController]
[Authorize]
[Route("faturamentos")]
public class FaturamentoController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("encerrar")]
    public async Task<ActionResult> EncerrarFaturamento(FinalizarFaturamentoRequest request)
    {
        var command = mapper.Map<FinalizarFaturamentoCommand>(request);
        
        var result = await mediator.Send(command);
        
        if (result.IsFailed)
        {
            if (result.HasError(e => e.HasMetadata("TipoErro", m => m.Equals("RequisicaoInvalida"))))
            {
                var errosDeValidacao = result.Errors
                    .SelectMany(e => e.Reasons.OfType<IError>())
                    .Select(e => e.Message);

                return Conflict(errosDeValidacao);
            }

            if (result.HasError(e => e.HasMetadata("TipoErro", m => m.Equals("RegistroNaoEncontradoErro"))))
            {
                var errosDeValidacao = result.Errors
                    .SelectMany(e => e.Reasons.OfType<IError>())
                    .Select(e => e.Message);

                return BadRequest(errosDeValidacao);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var response = mapper.Map<FinalizarFaturamentoResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarFaturamentosResponse>> SelecionarRegistros(
        [FromQuery] SelecionarFaturamentosRequest? request,
        CancellationToken cancellationToken
    )
    {
        var query = mapper.Map<SelecionarFaturamentosQuery>(request);

        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<SelecionarFaturamentosResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarFaturamentoPorIdResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = mapper.Map<SelecionarFaturamentoPorIdQuery>(id);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return NotFound(id);

        var response = mapper.Map<SelecionarFaturamentoPorIdResponse>(result.Value);

        return Ok(response);
    }
}
