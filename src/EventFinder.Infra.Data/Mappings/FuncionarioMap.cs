using EventFinder.Domain.Empresas;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class FuncionarioMap : EntityTypeConfiguration<Funcionario>
    {
        public override void Map(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("Funcionarios");
            builder.Property(x => x.Nome).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("varchar(11)").IsRequired();
            builder.Property(x => x.Facebook).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.Cargo).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Sexo).HasColumnType("varchar(1)").IsRequired();
            builder.HasOne(x => x.Empresa)
                 .WithMany(x => x.Funcionarios)
                 .HasForeignKey(x => x.EmpresaId)
                 .IsRequired();
            builder.HasOne(x => x.Equipe)
                 .WithMany(x => x.Funcionarios)
                 .HasForeignKey(x => x.EquipeId)
                 .IsRequired();
            builder.HasMany(x => x.Atividades)
                .WithOne(x => x.Funcionario)
                .HasForeignKey(x => x.FuncionarioId)
                .IsRequired(false);
            builder.HasMany(x => x.FuncionariosEventos)
                .WithOne(x => x.Funcionario)
                .HasForeignKey(x => x.FuncionarioId)
                .IsRequired();
            builder.HasMany(x => x.FuncionariosEventos)
                .WithOne(x => x.Funcionario)
                .HasForeignKey(x => x.FuncionarioId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);

        }
    }
}
