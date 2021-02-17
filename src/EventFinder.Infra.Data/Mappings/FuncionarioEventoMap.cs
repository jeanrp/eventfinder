using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class FuncionarioEventoMap : EntityTypeConfiguration<FuncionarioEvento>
    {
        public override void Map(EntityTypeBuilder<FuncionarioEvento> builder)
        {
            builder.HasKey(x => new { x.EventoId, x.FuncionarioId });
            builder.HasOne(x => x.Funcionario)
                         .WithMany(x => x.FuncionariosEventos)
                         .HasForeignKey(x => x.FuncionarioId)
                         .IsRequired();
            builder.HasOne(x => x.Evento)
                .WithMany(x => x.FuncionariosEventos)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.CascadeMode);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}
