using EventFinder.Domain.Clientes;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class ClientePromocaoMap : EntityTypeConfiguration<ClientePromocao>
    {
        public override void Map(EntityTypeBuilder<ClientePromocao> builder)
        {
            builder.ToTable("ClientesPromocoes");
            builder.HasKey(x => new { x.ClienteId, x.PromocaoId });
            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.ClientesPromocoes)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();
            builder.HasOne(x => x.Promocao)
                .WithMany(x => x.ClientesPromocoes)
                .HasForeignKey(x => x.PromocaoId)
                .IsRequired();
            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
