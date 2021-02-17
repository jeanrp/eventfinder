using EventFinder.Domain.Clientes;
using EventFinder.Domain.Usuarios;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public override void Map(EntityTypeBuilder<Cliente> builder)
        { 
            builder.ToTable("Clientes");
            builder.Property(x => x.Nome).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("varchar(11)").IsRequired();
            builder.Property(x => x.Facebook).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.Property(x => x.Sexo).HasColumnType("varchar(1)").IsRequired();
            builder.Property(x => x.EstadoCivil).HasColumnType("varchar(1)").IsRequired();
            builder.Property(x => x.EstiloPreferido).HasColumnType("varchar(100)");
            builder.Property(x => x.AtracaoPreferida).HasColumnType("varchar(100)");
            builder.HasOne(x => x.Usuario)
              .WithOne(x => x.Cliente)
              .HasForeignKey<Usuario>(x => x.ClienteId);
            builder.HasMany(x => x.Mensagens)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();
            builder.HasMany(x => x.IngressosComprados)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();
            builder.HasMany(x => x.Publicacoes)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();
            builder.HasMany(x => x.Avaliacoes)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();         
            builder.HasMany(x => x.Fotos)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired(false);

            builder.HasMany(x => x.ClientesPromocoes)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);

        }
    }
}
