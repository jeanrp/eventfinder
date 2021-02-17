using EventFinder.Domain.Clientes;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class FotoMap : EntityTypeConfiguration<Foto>
    {
        public override void Map(EntityTypeBuilder<Foto> builder)
        {
            builder.ToTable("Fotos");
            builder.Property(x => x.Descricao).HasColumnType("varchar(max)").IsRequired(false);
            builder.Property(x => x.Imagem) ;
            builder.HasOne(x => x.Evento)
                 .WithMany(x => x.Fotos)
                 .HasForeignKey(x => x.EventoId)
                 .IsRequired(false); 
            builder.HasOne(x => x.Empresa)
                 .WithMany(x => x.Fotos)
                 .HasForeignKey(x => x.EmpresaId)
                 .IsRequired(false);
            builder.HasOne(x => x.Cliente)
                 .WithMany(x => x.Fotos)
                 .HasForeignKey(x => x.ClienteId)
                 .IsRequired(false);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
