using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEstacionamento.Infra.Orm.ModuloVeiculo;

public class MapeadorVeiculoEmOrm : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Placa)
            .IsRequired();

        builder.Property(x => x.Modelo)
            .IsRequired();

        builder.Property(x => x.Cor)
            .IsRequired();

        builder.Property(x => x.Detalhes)
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired();

        builder.Property(x => x.Cpf)
            .IsRequired();

        builder.Property(x => x.Telefone)
            .IsRequired();

        builder.HasOne(x => x.Ticket)
            .WithOne(x => x.Veiculo)
            .HasForeignKey<Ticket>(t => t.VeiculoId)
            .IsRequired();

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}
