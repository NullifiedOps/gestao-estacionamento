using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoEstacionamento.Infra.Orm.Compartilhado;

public class AppDbContext(DbContextOptions options, ITenantProvider? tenantProvider = null) :
    IdentityDbContext<Usuario, Cargo, Guid>(options), IUnitOfWork
{
    //public DbSet<YYYY> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (tenantProvider is not null)
        {
            //modelBuilder.Entity<YYYY>()
            //    .HasQueryFilter(x => x.UsuarioId.Equals(tenantProvider.UsuarioId));
        }

        var assembly = typeof(AppDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task CommitAsync()
    {
        await SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        await Task.CompletedTask;
    }
}
