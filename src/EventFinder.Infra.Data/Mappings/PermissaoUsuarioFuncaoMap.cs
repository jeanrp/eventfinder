using EventFinder.Domain.Usuarios;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class PermissaoUsuarioFuncaoMap : EntityTypeConfiguration<PermissaoUsuarioFuncao>
    {
        public override void Map(EntityTypeBuilder<PermissaoUsuarioFuncao> builder)
        {
            builder.ToTable("PermissoesUsuariosFuncoes");
            builder.HasKey(x => new { x.FuncaoId, x.UsuarioId });
            builder.Property(x => x.Permissoes).HasColumnType("varchar(3)").IsRequired();
            builder.HasOne(x => x.Funcao)
                 .WithMany(x => x.Permissoes)
                 .HasForeignKey(x => x.FuncaoId)
                 .IsRequired();
            builder.HasOne(x => x.Usuario)
                 .WithMany(x => x.Permissoes)
                 .HasForeignKey(x => x.UsuarioId)
                 .IsRequired();
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
