using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventFinder.Infra.Data.Mappings
{
    public class CidadeMap : EntityTypeConfiguration<Cidade>
    {
        public override void Map(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("Cidades");
            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.HasOne(x => x.Estado)
                .WithMany(x => x.Cidades)
                .HasForeignKey(x => x.EstadoId)
                .IsRequired();
            builder.HasMany(x => x.Enderecos)
                .WithOne(x => x.Cidade)
                .HasForeignKey(x => x.CidadeId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);

        }
    }
}
