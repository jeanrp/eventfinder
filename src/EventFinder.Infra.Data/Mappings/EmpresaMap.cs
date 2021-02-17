using EventFinder.Domain.Empresas;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Usuarios;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public override void Map(EntityTypeBuilder<Empresa> builder)
        { 
            builder.ToTable("Empresas");
            builder.Property(x => x.RazaoSocial).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("varchar(11)").IsRequired();
            builder.Property(x => x.Facebook).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.Cnpj).HasColumnType("varchar(14)").IsRequired();
            builder.HasOne(c => c.Endereco)
                  .WithOne(c => c.Empresa)
                  .HasForeignKey<Empresa>(c => c.EnderecoId)
                  .IsRequired(false);
            builder.HasMany(x => x.Eventos)
                .WithOne(x => x.Empresa)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired();
            builder.HasMany(x => x.Fotos)
                .WithOne(x => x.Empresa)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired(false);
            builder.HasMany(x => x.Mensagens)
                .WithOne(x => x.Empresa)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired();
            builder.HasMany(x => x.Atividades)
             .WithOne(x => x.Empresa)
             .HasForeignKey(x => x.EmpresaId)
             .IsRequired();
            builder.HasMany(x => x.Funcionarios)
                .WithOne(x => x.Empresa)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired();
            builder.HasMany(x => x.Equipes)
              .WithOne(x => x.Empresa)
              .HasForeignKey(x => x.EmpresaId)
              .IsRequired();
            builder.HasOne(x => x.Usuario)
              .WithOne(x => x.Empresa)
              .HasForeignKey<Usuario>(x => x.EmpresaId);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);

        }
    }
}
