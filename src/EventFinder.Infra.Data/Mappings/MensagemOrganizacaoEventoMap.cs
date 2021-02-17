using EventFinder.Domain.Clientes;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class MensagemOrganizacaoEventoMap : EntityTypeConfiguration<MensagemOrganizacaoEvento>
    {
        public override void Map(EntityTypeBuilder<MensagemOrganizacaoEvento> builder)
        {
            builder.ToTable("MensagensOrganizacoesEventos");
            builder.HasKey(x => new { x.ClienteId, x.EmpresaId });
            builder.Property(x => x.DataHora).IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.Anexo).IsRequired(false);
            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Mensagens)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();
            builder.HasOne(x => x.Empresa)
                .WithMany(x => x.Mensagens)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired();
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
