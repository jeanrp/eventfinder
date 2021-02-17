using EventFinder.Domain.Clientes;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class PublicacaoMap : EntityTypeConfiguration<Publicacao>
    {
        public override void Map(EntityTypeBuilder<Publicacao> builder)
        {
            builder.ToTable("Publicacoes");
            builder.HasKey(x => new { x.ClienteId, x.EventoId });
            builder.Property(x => x.Descricao).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.DataHora).IsRequired();
            builder.Property(x => x.Anexo).IsRequired(false);
            builder.HasOne(x => x.Cliente)
                   .WithMany(x => x.Publicacoes)
                   .HasForeignKey(x => x.ClienteId)
                   .IsRequired();
            builder.HasOne(x => x.Evento)
                   .WithMany(x => x.Publicacoes)
                   .HasForeignKey(x => x.EventoId)
                   .IsRequired();
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
