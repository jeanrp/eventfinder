using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class EstadoMap : EntityTypeConfiguration<Estado>
    {
        public override void Map(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados");
            builder.Property(x => x.Uf).HasColumnType("varchar(2)");
            builder.HasMany(x => x.Cidades)
                .WithOne(x => x.Estado)
                .HasForeignKey(x => x.EstadoId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
