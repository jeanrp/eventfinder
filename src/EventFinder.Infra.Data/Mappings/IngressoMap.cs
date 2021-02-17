using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventFinder.Infra.Data.Mappings
{
    public class IngressoMap : EntityTypeConfiguration<Ingresso>
    {
        public override void Map(EntityTypeBuilder<Ingresso> builder)
        {
            builder.ToTable("Ingressos");
            builder.Property(x => x.Tipo).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Preco).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(x => x.Lote).IsRequired();
            builder.HasOne(x => x.Evento)
                .WithMany(x => x.Ingressos)
                .HasForeignKey(x => x.EventoId);
            builder.HasMany(x => x.IngressosComprados)
                .WithOne(x => x.Ingresso)
                .HasForeignKey(x => x.IngressoId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
