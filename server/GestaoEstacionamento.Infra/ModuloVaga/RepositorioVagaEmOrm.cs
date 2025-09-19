using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using GestaoEstacionamento.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GestaoEstacionamento.Infra.Orm.ModuloVaga;

public class RepositorioVagaEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Vaga>(contexto), IRepositorioVaga
{
    public override async Task<Vaga?> SelecionarRegistroPorIdAsync(Guid idRegistro)
    {
        return await registros
            .Include(x => x.Veiculo)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRegistro)); ;
    }

    public override async Task<List<Vaga>> SelecionarRegistrosAsync()
    {
        return await registros
            .Include(x => x.Veiculo)
            .ToListAsync();
    }

    public override async Task<List<Vaga>> SelecionarRegistrosAsync(int quantidade)
    {
        return await registros
            .Include(x => x.Veiculo)
            .Take(quantidade)
            .ToListAsync();
    }
}