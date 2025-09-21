using GestaoEstacionamento.Core.Dominio.ModuloFaturamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEstacionamento.Infra.Orm.ModuloFaturamento;

public class MapeadorFaturamentoEmOrm : IEntityTypeConfiguration<Faturamento>
{
    public void Configure(EntityTypeBuilder<Faturamento> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.PlacaVeiculo)
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.DataFaturamento)
            .IsRequired();

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}
