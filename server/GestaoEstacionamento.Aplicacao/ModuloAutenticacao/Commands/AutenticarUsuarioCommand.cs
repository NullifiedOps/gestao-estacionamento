using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;

public record AutenticarUsuarioCommand(string Email, string Senha) : IRequest<Result<AccessToken>>;
