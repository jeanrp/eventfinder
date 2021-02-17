using AutoMapper;
using EventFinder.Domain.Clientes.Commands;
using EventFinder.Domain.Empresas.Commands;
using EventFinder.Domain.Eventos.Commands;
using EventFinder.Services.Api.ViewModels;
using System;
using System.Collections.Generic;

namespace EventFinder.Services.Api.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {

            //Agregado Evento
            CreateMap<AtracaoViewModel, IncluirAtracaoCommand>();
            CreateMap<IngressoViewModel, IncluirIngressoCommand>();
            CreateMap<FotoViewModel, AdicionarFotoCommand>();
            CreateMap<EventoViewModel, IncluirEventoCommand>()
                .ConstructUsing(x =>
                {
                    var atracoes = Mapper.Map<IEnumerable<IncluirAtracaoCommand>>(x.Atracoes);
                    var ingressos = Mapper.Map<IEnumerable<IncluirIngressoCommand>>(x.Ingressos);
                    var fotos = Mapper.Map<IEnumerable<AdicionarFotoCommand>>(x.Fotos);
                    return new IncluirEventoCommand(x.Nome, x.Descricao, x.SubDescricao, x.DescPatrocinadores, x.DataHoraInicio, x.DataHoraFim, x.CategoriaId, x.EmpresaId, new IncluirEnderecoCommand(x.EnderecoId, x.Endereco.Logradouro, x.Endereco.Numero, x.Endereco.Complemento, x.Endereco.Cep, x.Endereco.Bairro, new IncluirCidadeCommand(x.Endereco.Cidade.Id, x.Endereco.Cidade.Nome, x.Endereco.Cidade.EstadoId)), atracoes, ingressos, fotos);
                });

            CreateMap<CidadeViewModel, IncluirCidadeCommand>()
            .ConstructUsing(x => new IncluirCidadeCommand(x.Id, x.Nome, x.EstadoId));
            CreateMap<EventoViewModel, AtualizarEventoCommand>()
                  .ConstructUsing(x => new AtualizarEventoCommand(x.Nome, x.Descricao, x.SubDescricao, x.DescPatrocinadores, x.DataHoraInicio, x.DataHoraFim, x.Situacao, x.CategoriaId));
            CreateMap<EventoViewModel, ExcluirEventoCommand>()
                .ConstructUsing(x => new ExcluirEventoCommand(x.Id));
            CreateMap<EnderecoViewModel, IncluirEnderecoCommand>()
                .ConstructUsing(x => new IncluirEnderecoCommand(x.Id, x.Logradouro, x.Numero, x.Complemento, x.Cep, x.Bairro, new IncluirCidadeCommand(x.Cidade.Id, x.Cidade.Nome, x.Cidade.EstadoId)));
            CreateMap<EnderecoViewModel, AtualizarEnderecoCommand>()
                .ConstructUsing(x => new AtualizarEnderecoCommand(x.Id, x.Logradouro, x.Numero, x.Complemento, x.Cep, x.Bairro, x.CidadeId));
            CreateMap<EnderecoViewModel, ExcluirEnderecoCommand>()
                .ConstructProjectionUsing(x => new ExcluirEnderecoCommand(x.Id));
            CreateMap<AtracaoViewModel, IncluirAtracaoCommand>()
                .ConstructUsing(x => new IncluirAtracaoCommand(x.Id, x.Nome, x.Estilo));
            CreateMap<AtracaoViewModel, ExcluirAtracaoCommand>()
                .ConstructUsing(x => new ExcluirAtracaoCommand(x.Id, x.EventoId));
            CreateMap<AtracaoViewModel, AtualizarAtracaoCommand>()
                .ConstructUsing(x => new AtualizarAtracaoCommand(x.Id, x.Nome, x.Estilo));
            CreateMap<IngressoViewModel, IncluirIngressoCommand>()
                .ConstructUsing(x => new IncluirIngressoCommand(x.Id, x.Preco, x.Tipo, x.Lote, x.EventoId));
            CreateMap<IngressoViewModel, AtualizarIngressoCommand>()
                .ConstructUsing(x => new AtualizarIngressoCommand(x.Id, x.Preco, x.Tipo, x.Lote, x.EventoId));
            CreateMap<IngressoViewModel, ExcluirIngressoCommand>()
                .ConstructUsing(x => new ExcluirIngressoCommand(x.Id));

            //Agregado empresa
            CreateMap<EmpresaViewModel, IncluirEmpresaCommand>()
                .ConstructUsing(x => new IncluirEmpresaCommand(x.Id, x.RazaoSocial, x.Email, x.Telefone, x.Facebook, x.Cnpj, x.EnderecoId, x.UsuarioId));
            CreateMap<EmpresaViewModel, EditarEmpresaCommand>()
                .ConstructUsing(x => new EditarEmpresaCommand(x.Id, x.RazaoSocial, x.Email, x.Telefone, x.Facebook, x.Cnpj));
            CreateMap<EmpresaViewModel, ExcluirEmpresaCommand>()
                .ConstructUsing(x => new ExcluirEmpresaCommand(x.Id));
            CreateMap<FuncionarioViewModel, IncluirFuncionarioCommand>()
              .ConstructUsing(x => new IncluirFuncionarioCommand(x.Id, x.Nome, x.Email, x.Telefone, x.Facebook, x.Cargo, x.Sexo, x.EmpresaId, x.EquipeId));
            CreateMap<FuncionarioViewModel, EditarFuncionarioCommand>()
                .ConstructUsing(x => new EditarFuncionarioCommand(x.Id, x.Nome, x.Email, x.Telefone, x.Facebook, x.Cargo, x.Sexo, x.EquipeId));
            CreateMap<FuncionarioViewModel, ExcluirFuncionarioCommand>()
                .ConstructUsing(x => new ExcluirFuncionarioCommand(x.Id));
            CreateMap<EquipeViewModel, IncluirEquipeCommand>()
          .ConstructUsing(x => new IncluirEquipeCommand(x.Id, x.Nome, x.Descricao, x.EmpresaId));
            CreateMap<EquipeViewModel, EditarEquipeCommand>()
                .ConstructUsing(x => new EditarEquipeCommand(x.Id, x.Nome, x.Descricao));
            CreateMap<EquipeViewModel, ExcluirEquipeCommand>()
                .ConstructUsing(x => new ExcluirEquipeCommand(x.Id));
            CreateMap<AtividadeViewModel, IncluirAtividadeCommand>()
          .ConstructUsing(x => new IncluirAtividadeCommand(x.Id, x.Descricao, x.Nome, x.DataHoraInicio, x.DataHoraFim, x.EmpresaId.Value));
            CreateMap<AtividadeViewModel, EditarAtividadeCommand>()
                .ConstructUsing(x => new EditarAtividadeCommand(x.Id, x.Descricao, x.Nome, x.DataHoraInicio, x.DataHoraFim, x.EmpresaId.Value));
            CreateMap<AtividadeViewModel, ExcluirAtividadeCommand>()
                .ConstructUsing(x => new ExcluirAtividadeCommand(x.Id));

            //Agregado Cliente
            CreateMap<AvaliacaoViewModel, AdicionarAvaliacaoCommand>()
                .ConstructUsing(x => new AdicionarAvaliacaoCommand(x.Nota, x.Descricao, x.EventoId, x.ClienteId));
            CreateMap<AvaliacaoViewModel, AtualizarAvaliacaoCommand>()
                .ConstructUsing(x => new AtualizarAvaliacaoCommand(x.Nota, x.Descricao, x.EventoId, x.ClienteId));
            CreateMap<AvaliacaoViewModel, ExcluirAvaliacaoCommand>()
                .ConstructUsing(x => new ExcluirAvaliacaoCommand(x.EventoId, x.ClienteId));
            CreateMap<ClienteViewModel, AdicionarClienteCommand>()
                .ConstructUsing(x => new AdicionarClienteCommand(x.Id, x.Nome, x.Email, x.Senha, x.Telefone, x.Facebook, x.DataNascimento, x.Sexo, x.EstadoCivil, x.AtracaoPreferida, x.EstiloPreferido));
            CreateMap<ClienteViewModel, AtualizarClienteCommand>()
                .ConstructUsing(x => new AtualizarClienteCommand(x.Id, x.Nome, x.EstiloPreferido, x.Senha, x.Telefone, x.Facebook, x.DataNascimento, x.Senha, x.EstadoCivil, x.AtracaoPreferida, x.EstiloPreferido));
            CreateMap<ClienteViewModel, ExcluirClienteCommand>()
                .ConstructUsing(x => new ExcluirClienteCommand(x.Id));
            CreateMap<FotoViewModel, AdicionarFotoCommand>()
                .ConstructUsing(x => new AdicionarFotoCommand(x.Id, x.Descricao, x.Imagem, x.EventoId, x.ClienteId, x.EmpresaId));
            CreateMap<FotoViewModel, ExcluirFotoCommand>()
                .ConstructUsing(x => new ExcluirFotoCommand(x.Id));
            CreateMap<IngressoCompradoViewModel, AdicionarIngressoCompradoCommand>()
                .ConstructUsing(x => new AdicionarIngressoCompradoCommand(x.Quantidade, x.NomeEvento, x.ValorTotal, x.ClienteId, x.IngressoId));
            CreateMap<MensagemOrganizacaoEventoViewModel, AdicionarMensagemOrganizacaoEventoCommand>()
                .ConstructUsing(x => new AdicionarMensagemOrganizacaoEventoCommand(x.Descricao, x.Anexo, x.ClienteId, x.EmpresaId));
            CreateMap<PublicacaoViewModel, AdicionarPublicacaoCommand>()
                .ConstructUsing(x => new AdicionarPublicacaoCommand(x.Descricao, x.Anexo, x.ClienteId, x.EventoId));
            CreateMap<PublicacaoViewModel, AtualizarPublicacaoCommand>()
                .ConstructUsing(x => new AtualizarPublicacaoCommand(x.Descricao, x.Anexo, x.ClienteId, x.EventoId));
            CreateMap<PublicacaoViewModel, ExcluirPublicacaoCommand>()
                .ConstructUsing(x => new ExcluirPublicacaoCommand(x.ClienteId, x.EventoId));
        }
    }
}
