using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEstacionamento.Infra.Orm.ModuloVeiculo;

public class MapeadorTicketEmOrm : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Numero)
            .IsRequired();

        builder.Property(x => x.DataEntrada)
            .IsRequired();

        builder.Property(x => x.DataSaida)
            .IsRequired(false);

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}
