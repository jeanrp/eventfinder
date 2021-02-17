using EventFinder.Domain.Clientes;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class AvaliacaoMap : EntityTypeConfiguration<Avaliacao>
    {
        public override void Map(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacoes");
            builder.HasKey(x => new { x.ClienteId, x.EventoId });
            builder.Property(x => x.Nota).HasColumnType("decimal(2,2)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(500)").IsRequired(false);
            builder.Property(x => x.DataHora).IsRequired();
            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Avaliacoes)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();
            builder.HasOne(x => x.Evento)
                .WithMany(x => x.Avaliacoes)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);

        }
    }
}
