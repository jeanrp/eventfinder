using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventFinder.Infra.Data.Mappings
{
    public class AtracaoEventoMap : EntityTypeConfiguration<AtracaoEvento>
    {
        public override void Map(EntityTypeBuilder<AtracaoEvento> builder)
        {
            builder.ToTable("AtracoesEventos");

            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.AtracaoId, x.EventoId });

            builder.HasOne(x => x.Evento)
                .WithMany(x => x.AtracoesEventos)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();

            builder.HasOne(x => x.Atracao)
                .WithMany(x => x.AtracoesEventos)
                .HasForeignKey(x => x.AtracaoId)
                .IsRequired();

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
