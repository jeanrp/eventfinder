using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventFinder.Infra.Data.Mappings
{
    public class AtracaoMap : EntityTypeConfiguration<Atracao>
    {
        public override void Map(EntityTypeBuilder<Atracao> builder)
        {          
            builder.ToTable("Atracoes");
            builder.Property(x => x.Nome).HasColumnType("varchar(150)");
            builder.Property(x => x.Estilo).HasColumnType("varchar(100)");
            builder.HasMany(x => x.AtracoesEventos)
                .WithOne(x => x.Atracao)
                .HasForeignKey(x => x.AtracaoId)
                .IsRequired(true);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
