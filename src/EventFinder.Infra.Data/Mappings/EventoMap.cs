using EventFinder.Domain.Eventos;
using EventFinder.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFinder.Infra.Data.Mappings
{
    public class EventoMap : EntityTypeConfiguration<Evento>
    {
        public override void Map(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("Eventos");
            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.SubDescricao).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(x => x.DescPatrocinadores).HasColumnType("varchar(50)").IsRequired(false);
            builder.Property(x => x.Situacao).HasColumnType("varchar(1)").IsRequired();
            builder.Property(x => x.DataInclusao).IsRequired();
            builder.Property(x => x.DataHoraInicio).IsRequired();
            builder.Property(x => x.DataHoraFim).IsRequired();
            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Eventos)
                .HasForeignKey(x => x.CategoriaId)
                .IsRequired();
            builder.HasOne(x => x.Empresa)
                .WithMany(x => x.Eventos)
                .HasForeignKey(x => x.EmpresaId)
                .IsRequired();
            builder.HasOne(c => c.Endereco)
                 .WithOne(c => c.Evento)
                 .HasForeignKey<Evento>(c => c.EnderecoId)
                 .IsRequired();
            builder.HasMany(x => x.Avaliacoes)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.HasMany(x => x.Ingressos)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();  
            builder.HasMany(x => x.Promocoes)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.HasMany(x => x.FuncionariosEventos)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.HasMany(x => x.AtracoesEventos)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.HasMany(x => x.Fotos)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired(false);
            builder.HasMany(x => x.Publicacoes)
                .WithOne(x => x.Evento)
                .HasForeignKey(x => x.EventoId)
                .IsRequired();
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
