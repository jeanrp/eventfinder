using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public override void Map(EntityTypeBuilder<Categoria> builder)
        { 
            builder.ToTable("Categorias");
            builder.Property(x => x.Descricao).HasColumnType("varchar(100)");
            builder.Property(x => x.Classificacao).IsRequired();
            builder.HasMany(x => x.Eventos)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
