using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using GestaoEstacionamento.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GestaoEstacionamento.Infra.Orm.ModuloVeiculo;

public class RepositorioVeiculoEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Veiculo>(contexto), IRepositorioVeiculo
{
    public override async Task<Veiculo?> SelecionarRegistroPorIdAsync(Guid idRegistro)
    {
        return await registros
            .Include(x => x.Ticket)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRegistro));
    }

    public override async Task<List<Veiculo>> SelecionarRegistrosAsync()
    {
        return await registros
            .Include(x => x.Ticket)
            .ToListAsync();
    }

    public override async Task<List<Veiculo>> SelecionarRegistrosAsync(int quantidade)
    {
        return await registros
            .Include(x => x.Ticket)
            .Take(quantidade)
            .ToListAsync();
    }
}