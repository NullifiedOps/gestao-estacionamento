using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEstacionamento.Infra.Orm.ModuloVaga;

public class MapeadorVagaEmOrm : IEntityTypeConfiguration<Vaga>
{
    public void Configure(EntityTypeBuilder<Vaga> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Zona)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.Identificador)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(x => x.Veiculo)
            .WithOne()
            .HasForeignKey<Vaga>(x => x.VeiculoId)
            .IsRequired(false);

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}
