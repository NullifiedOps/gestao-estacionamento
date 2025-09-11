using GestaoEstacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GestaoEstacionamento.Core.Aplicacao.ModuloAutenticacao.Handlers;
public class SairCommandHandler(
    SignInManager<Usuario> signInManager
) : IRequestHandler<SairCommand, Result>
{
    public async Task<Result> Handle(SairCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return Result.Ok();
    }
}
