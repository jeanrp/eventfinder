using EventFinder.Domain.Funcionarios;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class AtividadeMap : EntityTypeConfiguration<Atividade>
    {
        public override void Map(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("Atividades");
            builder.Property(x => x.DataHoraInicio).IsRequired();
            builder.Property(x => x.DataHoraFim).IsRequired();
            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(max)").IsRequired();

            builder.Ignore(x => x.ValidationResult); 
            builder.Ignore(x => x.CascadeMode);

            builder.HasOne(x => x.Funcionario)
                .WithMany(x => x.Atividades)
                .HasForeignKey(x => x.FuncionarioId)
                .IsRequired(false);

            builder.HasOne(x => x.Empresa)
             .WithMany(x => x.Atividades)
             .HasForeignKey(x => x.EmpresaId)
             .IsRequired(true);
        }
    }
}
