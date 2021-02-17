using AutoMapper;
using EventFinder.Domain.Clientes.Commands;
using EventFinder.Domain.Clientes.Commands.Handler;
using EventFinder.Domain.Clientes.Events;
using EventFinder.Domain.Clientes.Events.Handler;
using EventFinder.Domain.Clientes.Repository;
using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Empresas.Commands;
using EventFinder.Domain.Empresas.Commands.Handler;
using EventFinder.Domain.Empresas.Events;
using EventFinder.Domain.Empresas.Events.Handler;
using EventFinder.Domain.Empresas.Repository;
using EventFinder.Domain.Eventos.Commands;
using EventFinder.Domain.Eventos.Commands.Handler;
using EventFinder.Domain.Eventos.Events;
using EventFinder.Domain.Eventos.Repository;
using EventFinder.Domain.Handlers;
using EventFinder.Domain.Interfaces;
using EventFinder.Domain.Usuarios.Repository;
using EventFinder.Infra.CrossCutting.IoC.Security;
using EventFinder.Infra.Data.Context;
using EventFinder.Infra.Data.Repository;
using EventFinder.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EventFinder.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET 
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));


            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();



            // Domain - Commands - Evento
            services.AddScoped<INotificationHandler<AtualizarAtracaoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarEnderecoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarIngressoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirAtracaoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirEnderecoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirEventoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirIngressoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirAtracaoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirEnderecoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirEventoCommand>, EventoCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirIngressoCommand>, EventoCommandHandler>();


            //// Domain - Events - Evento
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<AtracaoAdicionadaEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<AtracaoAtualizadaEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<AtracaoExcluidaEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<EnderecoEventoAdicionadoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<EnderecoEventoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<EnderecoEventoExcluidoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<EventoAdicionadoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<EventoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<EventoExcluidoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<IngressoAdicionadoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<IngressoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<INotificationHandler<IngressoExcluidoEvent>, EventoEventHandler>();

            // Domain - Commands - Empresa
            services.AddScoped<INotificationHandler<EditarAtividadeCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<EditarEmpresaCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<EditarEquipeCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<EditarFuncionarioCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirAtividadeCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirEmpresaCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirEquipeCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirFuncionarioCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirAtividadeCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirEmpresaCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirEquipeCommand>, EmpresaCommandHandler>();
            services.AddScoped<INotificationHandler<IncluirFuncionarioCommand>, EmpresaCommandHandler>();


            // Domain - Events - Empresa
            services.AddScoped<INotificationHandler<AtividadeAdicionadaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<AtividadeAtualizadaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<AtividadeExcluidaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<EmpresaAdicionadaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<EmpresaAtualizadaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<EmpresaExcluidaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<EquipeAdicionadaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<EquipeAtualizadaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<EquipeExcluidaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<FuncionarioAdicionadoEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<FuncionarioAtualizadoEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<FuncionarioExcluidoEvent>, EmpresaEventHandler>();


            // Domain - Commands - Cliente
            services.AddScoped<INotificationHandler<AdicionarAvaliacaoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AdicionarClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AdicionarFotoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AdicionarIngressoCompradoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AdicionarMensagemOrganizacaoEventoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AdicionarPublicacaoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarAvaliacaoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarPublicacaoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirAvaliacaoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirFotoCommand>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirPublicacaoCommand>, ClienteCommandHandler>();


            // Domain - Events - Empresa
            services.AddScoped<INotificationHandler<AvaliacaoAdicionadaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<AvaliacaoAtualizadaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<AvaliacaoExcluidaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteAdicionadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteAtualizadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteExcluidoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<FotoAdicionadaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<FotoExcluidaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<IngressoCompradoAdicionadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<MensagemOrganizacaoEventoAdicionadoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<PublicacaoAdicionadaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<PublicacaoAtualizadaEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<PublicacaoExcluidaEvent>, ClienteEventHandler>();

            //// Infra - Data
             services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Security.IUser, User>();
            services.AddScoped<EventosContext>();            

        }
    }
}
