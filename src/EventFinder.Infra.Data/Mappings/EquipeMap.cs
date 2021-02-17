using EventFinder.Domain.Empresas;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class EquipeMap : EntityTypeConfiguration<Equipe>
    {
        public override void Map(EntityTypeBuilder<Equipe> builder)
        {
            builder.ToTable("Equipes");
            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(500)").IsRequired(false);
            builder.HasMany(x => x.Funcionarios)
                .WithOne(x => x.Equipe)
                .HasForeignKey(x => x.EquipeId)
                .IsRequired();
            builder.HasOne(x => x.Empresa)
                .WithMany(x => x.Equipes)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
