using EventFinder.Domain.Usuarios;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class FuncaoMap : EntityTypeConfiguration<Funcao>
    {
        public override void Map(EntityTypeBuilder<Funcao> builder)
        { 
            builder.ToTable("Funcoes");
            builder.Property(x => x.Descricao).HasColumnType("varchar(100)").IsRequired();
            builder.HasMany(x => x.Permissoes)
                .WithOne(x => x.Funcao)
                .HasForeignKey(x => x.FuncaoId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
