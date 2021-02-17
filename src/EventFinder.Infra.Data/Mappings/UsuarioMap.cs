using EventFinder.Domain.Clientes;
using EventFinder.Domain.Usuarios;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public override void Map(EntityTypeBuilder<Usuario> builder)
        {         
            builder.ToTable("Usuarios");
            builder.Property(x => x.NomeRazaoSocial).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.DataHoraCadastro).IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Senha).HasColumnType("varchar(150)").IsRequired();
            builder.HasMany(x => x.Permissoes)
                   .WithOne(x => x.Usuario)
                   .HasForeignKey(x => x.UsuarioId)
                   .IsRequired();
            builder.HasOne(x => x.Cliente)
                .WithOne(x => x.Usuario)
                .HasForeignKey<Usuario>(x => x.ClienteId);
            builder.HasOne(x => x.Empresa)
              .WithOne(x => x.Usuario)
              .HasForeignKey<Usuario>(x => x.EmpresaId);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
