using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class PromocaoMap : EntityTypeConfiguration<Promocao>
    {
        public override void Map(EntityTypeBuilder<Promocao> builder)
        {
            builder.ToTable("Promocoes");
            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.NomeVencedor).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.DataHoraInicio).IsRequired();
            builder.Property(x => x.DataHoraFim).IsRequired();
            builder.Property(x => x.Situacao).HasColumnType("varchar(1)").IsRequired();
            builder.HasOne(x => x.Evento)
                       .WithMany(x => x.Promocoes)
                       .HasForeignKey(x => x.EventoId)
                       .IsRequired();
            builder.HasMany(x => x.ClientesPromocoes)
                .WithOne(x => x.Promocao)
                .HasForeignKey(x => x.PromocaoId)
                .IsRequired();            
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
