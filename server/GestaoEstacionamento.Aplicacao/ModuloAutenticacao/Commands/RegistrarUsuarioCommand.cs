using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using FluentResults;
using MediatR;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;

public record RegistrarUsuarioCommand(string NomeCompleto, string Email, string Senha, string ConfirmarSenha)
: IRequest<Result<AccessToken>>;
