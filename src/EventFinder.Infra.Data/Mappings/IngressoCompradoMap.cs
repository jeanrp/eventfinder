using EventFinder.Domain.Clientes;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class IngressoCompradoMap : EntityTypeConfiguration<IngressoComprado>
    {
        public override void Map(EntityTypeBuilder<IngressoComprado> builder)
        {
            builder.ToTable("IngressosComprados");
            builder.HasKey(x => new { x.ClienteId, x.IngressoId });
            builder.Property(x => x.DataHora).IsRequired();
            builder.Property(x => x.Quantidade).IsRequired();
            builder.Property(x => x.NomeEvento).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.ValorTotal).HasColumnType("decimal(8,2)").IsRequired();
            builder.HasOne(x => x.Cliente)
                 .WithMany(x => x.IngressosComprados)
                 .HasForeignKey(x => x.ClienteId)
                 .IsRequired();
            builder.HasOne(x => x.Ingresso)
                 .WithMany(x => x.IngressosComprados)
                 .HasForeignKey(x => x.IngressoId)
                 .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
            builder.Ignore(x => x.Id);
        }
    }
}
