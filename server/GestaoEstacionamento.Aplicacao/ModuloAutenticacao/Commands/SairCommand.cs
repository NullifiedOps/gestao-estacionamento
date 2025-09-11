using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;

public record SairCommand : IRequest<Result>;
