using EventFinder.Domain.Clientes;
using EventFinder.Domain.Empresas;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Funcionarios;
using EventFinder.Domain.Usuarios;
using EventFinder.Infra.Data.Extensions;
using EventFinder.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace EventFinder.Infra.Data.Context
{
    public class EventosContext : DbContext
    {

        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<AtracaoEvento> AtracoesEventos { get; set; }
        public DbSet<Atracao> Atracoes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClientePromocao> ClientesPromocoes { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<IngressoComprado> IngressosComprados { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }
        public DbSet<MensagemOrganizacaoEvento> MensagensOrganizacoesEventos { get; set; }
        public DbSet<PermissaoUsuarioFuncao> PermissoesUsuariosFuncoes { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<FuncionarioEvento> FuncionarioEventos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetForeignKeys())
                 .Where(fk => !fk.IsUnique && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.AddConfiguration(new AtividadeMap());
            modelBuilder.AddConfiguration(new AtracaoEventoMap());
            modelBuilder.AddConfiguration(new AtracaoMap());
            modelBuilder.AddConfiguration(new AvaliacaoMap());
            modelBuilder.AddConfiguration(new CategoriaMap());
            modelBuilder.AddConfiguration(new CidadeMap());
            modelBuilder.AddConfiguration(new ClienteMap());
            modelBuilder.AddConfiguration(new ClientePromocaoMap());
            modelBuilder.AddConfiguration(new EmpresaMap());
            modelBuilder.AddConfiguration(new EnderecoMap());
            modelBuilder.AddConfiguration(new EquipeMap());
            modelBuilder.AddConfiguration(new EstadoMap());
            modelBuilder.AddConfiguration(new EventoMap());
            modelBuilder.AddConfiguration(new FotoMap());
            modelBuilder.AddConfiguration(new FuncaoMap());
            modelBuilder.AddConfiguration(new FuncionarioMap());
            modelBuilder.AddConfiguration(new IngressoCompradoMap());
            modelBuilder.AddConfiguration(new IngressoMap());
            modelBuilder.AddConfiguration(new MensagemOrganizacaoEventoMap());
            modelBuilder.AddConfiguration(new PermissaoUsuarioFuncaoMap());
            modelBuilder.AddConfiguration(new PromocaoMap());
            modelBuilder.AddConfiguration(new PublicacaoMap());
            modelBuilder.AddConfiguration(new UsuarioMap());
            modelBuilder.AddConfiguration(new FuncionarioEventoMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("EventFinderConexao"));
        }

        public override int SaveChanges()
        {
            var validationErrors = ChangeTracker
                 .Entries<IValidatableObject>()
                 .SelectMany(e => e.Entity.Validate(null))
                 .Where(r => r != ValidationResult.Success);

            if (validationErrors.Any())
            {

            }
            return base.SaveChanges();
        }
    }
}
