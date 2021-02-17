﻿// <auto-generated />
using EventFinder.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EventFinder.Infra.Data.Migrations
{
    [DbContext(typeof(EventosContext))]
    [Migration("20171115130139_ChavesEstrangeirasNoUsuario")]
    partial class ChavesEstrangeirasNoUsuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventFinder.Domain.Clientes.Avaliacao", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("EventoId");

                    b.Property<DateTime>("DataHora");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<decimal>("Nota")
                        .HasColumnType("decimal(2,2)");

                    b.HasKey("ClienteId", "EventoId");

                    b.HasIndex("EventoId");

                    b.ToTable("Avaliacoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AtracaoPreferida")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("EstadoCivil")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("EstiloPreferido")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Facebook")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.ClientePromocao", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("PromocaoId");

                    b.HasKey("ClienteId", "PromocaoId");

                    b.HasIndex("PromocaoId");

                    b.ToTable("ClientesPromocoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.Foto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClienteId");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.Property<Guid?>("EmpresaId");

                    b.Property<Guid?>("EventoId");

                    b.Property<byte[]>("Imagem");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("EventoId");

                    b.ToTable("Fotos");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.IngressoComprado", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("IngressoId");

                    b.Property<DateTime>("DataHora");

                    b.Property<string>("NomeEvento")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Quantidade");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("ClienteId", "IngressoId");

                    b.HasIndex("IngressoId");

                    b.ToTable("IngressosComprados");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.MensagemOrganizacaoEvento", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("EmpresaId");

                    b.Property<byte[]>("Anexo");

                    b.Property<DateTime>("DataHora");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.HasKey("ClienteId", "EmpresaId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("MensagensOrganizacoesEventos");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.Publicacao", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("EventoId");

                    b.Property<byte[]>("Anexo");

                    b.Property<DateTime>("DataHora");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.HasKey("ClienteId", "EventoId");

                    b.HasIndex("EventoId");

                    b.ToTable("Publicacoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Empresas.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid?>("EnderecoId");

                    b.Property<string>("Facebook")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("EventFinder.Domain.Empresas.Equipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("EmpresaId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Equipes");
                });

            modelBuilder.Entity("EventFinder.Domain.Empresas.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("EmpresaId");

                    b.Property<Guid>("EquipeId");

                    b.Property<string>("Facebook")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("EquipeId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Atracao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estilo")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Atracoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.AtracaoEvento", b =>
                {
                    b.Property<Guid>("AtracaoId");

                    b.Property<Guid>("EventoId");

                    b.HasKey("AtracaoId", "EventoId");

                    b.HasIndex("EventoId");

                    b.ToTable("AtracoesEventos");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Classificacao");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Cidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EstadoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<Guid>("CidadeId");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(500)");

                    b.Property<Guid?>("EmpresaId");

                    b.Property<Guid?>("EventoId");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("EmpresaId")
                        .IsUnique()
                        .HasFilter("[EmpresaId] IS NOT NULL");

                    b.HasIndex("EventoId")
                        .IsUnique()
                        .HasFilter("[EventoId] IS NOT NULL");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Estado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Uf")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Evento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoriaId");

                    b.Property<DateTime>("DataHoraFim");

                    b.Property<DateTime>("DataHoraInicio");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("DescPatrocinadores")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("EmpresaId");

                    b.Property<Guid>("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("SubDescricao")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.FuncionarioEvento", b =>
                {
                    b.Property<Guid>("EventoId");

                    b.Property<Guid>("FuncionarioId");

                    b.HasKey("EventoId", "FuncionarioId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("FuncionarioEventos");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Ingresso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EventoId");

                    b.Property<short>("Lote");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Ingressos");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Promocao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClienteId");

                    b.Property<DateTime>("DataHoraFim");

                    b.Property<DateTime>("DataHoraInicio");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("EventoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NomeVencedor")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Promocoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Funcionarios.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHoraFim");

                    b.Property<DateTime>("DataHoraInicio");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("EmpresaId");

                    b.Property<Guid?>("FuncionarioId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("EventFinder.Domain.Usuarios.Funcao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Funcoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Usuarios.PermissaoUsuarioFuncao", b =>
                {
                    b.Property<Guid?>("FuncaoId");

                    b.Property<Guid?>("UsuarioId");

                    b.Property<string>("Permissoes")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.HasKey("FuncaoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PermissoesUsuariosFuncoes");
                });

            modelBuilder.Entity("EventFinder.Domain.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClienteId");

                    b.Property<DateTime>("DataHoraCadastro");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid?>("EmpresaId");

                    b.Property<string>("NomeRazaoSocial")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique()
                        .HasFilter("[ClienteId] IS NOT NULL");

                    b.HasIndex("EmpresaId")
                        .IsUnique()
                        .HasFilter("[EmpresaId] IS NOT NULL");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.Avaliacao", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.ClientePromocao", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("ClientesPromocoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Eventos.Promocao", "Promocao")
                        .WithMany("ClientesPromocoes")
                        .HasForeignKey("PromocaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.Foto", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Fotos")
                        .HasForeignKey("ClienteId");

                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithMany("Fotos")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("Fotos")
                        .HasForeignKey("EventoId");
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.IngressoComprado", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("IngressosComprados")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Eventos.Ingresso", "Ingresso")
                        .WithMany("IngressosComprados")
                        .HasForeignKey("IngressoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.MensagemOrganizacaoEvento", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Mensagens")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithMany("Mensagens")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Clientes.Publicacao", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Publicacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("Publicacoes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Empresas.Equipe", b =>
                {
                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithMany("Equipes")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Empresas.Funcionario", b =>
                {
                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Empresas.Equipe", "Equipe")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.AtracaoEvento", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Atracao", "Atracao")
                        .WithMany("AtracoesEventos")
                        .HasForeignKey("AtracaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("AtracoesEventos")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Cidade", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Estado", "Estado")
                        .WithMany("Cidades")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Endereco", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Cidade", "Cidade")
                        .WithMany("Enderecos")
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithOne("Endereco")
                        .HasForeignKey("EventFinder.Domain.Eventos.Endereco", "EmpresaId");

                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithOne("Endereco")
                        .HasForeignKey("EventFinder.Domain.Eventos.Endereco", "EventoId");
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Evento", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Categoria", "Categoria")
                        .WithMany("Eventos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithMany("Eventos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.FuncionarioEvento", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("FuncionariosEventos")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Empresas.Funcionario", "Funcionario")
                        .WithMany("FuncionariosEventos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Ingresso", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("Ingressos")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Eventos.Promocao", b =>
                {
                    b.HasOne("EventFinder.Domain.Eventos.Evento", "Evento")
                        .WithMany("Promocoes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EventFinder.Domain.Funcionarios.Atividade", b =>
                {
                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithMany("Atividades")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EventFinder.Domain.Empresas.Funcionario", "Funcionario")
                        .WithMany("Atividades")
                        .HasForeignKey("FuncionarioId");
                });

            modelBuilder.Entity("EventFinder.Domain.Usuarios.PermissaoUsuarioFuncao", b =>
                {
                    b.HasOne("EventFinder.Domain.Usuarios.Funcao", "Funcao")
                        .WithMany("Permissoes")
                        .HasForeignKey("FuncaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventFinder.Domain.Usuarios.Usuario", "Usuario")
                        .WithMany("Permissoes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventFinder.Domain.Usuarios.Usuario", b =>
                {
                    b.HasOne("EventFinder.Domain.Clientes.Cliente", "Cliente")
                        .WithOne("Usuario")
                        .HasForeignKey("EventFinder.Domain.Usuarios.Usuario", "ClienteId");

                    b.HasOne("EventFinder.Domain.Empresas.Empresa", "Empresa")
                        .WithOne("Usuario")
                        .HasForeignKey("EventFinder.Domain.Usuarios.Usuario", "EmpresaId");
                });
#pragma warning restore 612, 618
        }
    }
}
