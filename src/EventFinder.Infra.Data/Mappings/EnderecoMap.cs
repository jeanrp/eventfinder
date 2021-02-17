using EventFinder.Domain.Empresas;
using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class EnderecoMap : EntityTypeConfiguration<Endereco>
    {
        public override void Map(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");
            builder.Property(x => x.Logradouro).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Numero).HasColumnType("varchar(10)").IsRequired();
            builder.Property(x => x.Complemento).HasColumnType("varchar(500)").IsRequired(false);
            builder.Property(x => x.Cep).HasColumnType("varchar(8)").IsRequired();
            builder.Property(x => x.Bairro).HasColumnType("varchar(150)").IsRequired();
            builder.HasOne(x => x.Cidade)
                .WithMany(x => x.Enderecos)
                .HasForeignKey(x => x.CidadeId)
                .IsRequired();
            builder.HasOne(c => c.Evento)
               .WithOne(c => c.Endereco)
               .HasForeignKey<Evento>(c => c.EnderecoId)
               .IsRequired();
            builder.HasOne(c => c.Empresa)
               .WithOne(c => c.Endereco)
               .HasForeignKey<Empresa>(c => c.EnderecoId)
               .IsRequired(false);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
