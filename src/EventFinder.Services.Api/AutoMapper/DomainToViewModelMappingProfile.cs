using AutoMapper;
using EventFinder.Domain.Clientes;
using EventFinder.Domain.Empresas;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Eventos.Commands;
using EventFinder.Domain.Funcionarios;
using EventFinder.Domain.Usuarios;
using EventFinder.Services.Api.ViewModels;
using System.Collections.Generic;

namespace EventFinder.Services.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Atividade, AtividadeViewModel>();
            CreateMap<Atracao, AtracaoViewModel>();
            CreateMap<Avaliacao, AvaliacaoViewModel>();
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<Empresa, EmpresaViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Equipe, EquipeViewModel>();
            CreateMap<Foto, FotoViewModel>();
            CreateMap<Funcionario, FuncionarioViewModel>();
            CreateMap<IngressoComprado, IngressoCompradoViewModel>();
            CreateMap<Ingresso, IngressoViewModel>();
            CreateMap<Evento, EventoViewModel>();
            CreateMap<MensagemOrganizacaoEvento, MensagemOrganizacaoEventoViewModel>();
            CreateMap<Promocao, PromocaoViewModel>();
            CreateMap<Publicacao, PublicacaoViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<Estado, EstadoViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Cidade, CidadeViewModel>();
            

        }
    }
}
