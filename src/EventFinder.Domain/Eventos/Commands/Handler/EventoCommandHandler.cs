using EventFinder.Domain.Clientes;
using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Core.Utils;
using EventFinder.Domain.Eventos.Events;
using EventFinder.Domain.Eventos.Repository;
using EventFinder.Domain.Handlers;
using EventFinder.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Eventos.Commands.Handler
{
    public class EventoCommandHandler : CommandHandler,
        INotificationHandler<IncluirEventoCommand>,
        INotificationHandler<IncluirEnderecoCommand>,
        INotificationHandler<IncluirAtracaoCommand>,
        INotificationHandler<IncluirIngressoCommand>,
        INotificationHandler<AtualizarEventoCommand>,
        INotificationHandler<AtualizarAtracaoCommand>,
        INotificationHandler<AtualizarEnderecoCommand>,
        INotificationHandler<AtualizarIngressoCommand>,
        INotificationHandler<ExcluirIngressoCommand>,
        INotificationHandler<ExcluirAtracaoCommand>,
        INotificationHandler<ExcluirEventoCommand>,
        INotificationHandler<ExcluirEnderecoCommand>
    {

        private readonly IEventoRepository _eventoRepository;
        private readonly IMediatorHandler _mediator;

        public EventoCommandHandler(IEventoRepository eventoRepository,
            IUnitOfWork uow,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> cmds) : base(uow, mediator, cmds)
        {
            _mediator = mediator;
            _eventoRepository = eventoRepository;
        }


        public void Handle(ExcluirEventoCommand cmd)
        {
            Evento evento = _eventoRepository.ObterEventoCompletoPaginaPorId(cmd.Id);

            if (evento == null) return;

            List<Atracao> atracoes = new List<Atracao>();

            evento.AtracoesEventos.ToList().ForEach(x =>
                 {
                     atracoes.Add(x.Atracao);
                     _eventoRepository.RemoverAtracaoEvento(x.AtracaoId, x.EventoId);
                 });
            foreach (var atracao in atracoes)
                _eventoRepository.RemoverAtracao(atracao.Id);

            _eventoRepository.RemoverEndereco(cmd.EnderecoId);
            _eventoRepository.Remover(cmd.Id); 

            if (Commit())
            {
                _mediator.PublicarEvento(new AtracaoExcluidaEvent(cmd.Id));
            }
        }

        public void Handle(ExcluirAtracaoCommand cmd)
        {
            Atracao atracao = AtracaoExistente(cmd.Id, cmd.MessageType);
            Evento evento = EventoExistente(cmd.EventoId, cmd.MessageType);

            if (atracao == null || evento == null) return;

            _eventoRepository.RemoverAtracaoEvento(cmd.Id, cmd.EventoId);

            _eventoRepository.RemoverAtracao(cmd.Id);

            if (Commit())
            {
                _mediator.PublicarEvento(new AtracaoExcluidaEvent(cmd.Id));
            }
        }

        public void Handle(ExcluirIngressoCommand cmd)
        {
            Ingresso ingresso = IngressoExistente(cmd.Id, cmd.MessageType);

            if (ingresso == null) return;

            _eventoRepository.RemoverIngresso(cmd.Id);

            if (Commit())
            {
                _mediator.PublicarEvento(new IngressoExcluidoEvent(cmd.Id));
            }
        }

        public void Handle(AtualizarEnderecoCommand cmd)
        {
            var endereco = EnderecoExistente(cmd.Id, cmd.MessageType);

            if (endereco == null) return;

            endereco.AtualizarEndereco(cmd.Logradouro, cmd.Numero, cmd.Complemento, cmd.Cep, cmd.Bairro, cmd.CidadeId);

            if (!endereco.IsValid())
            {
                NotificarValidacoesErro(endereco.ValidationResult);
                return;
            }

            _eventoRepository.AlterarEndereco(endereco);

            if (Commit())
                _mediator.PublicarEvento(new EnderecoEventoAtualizadoEvent(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Cep, endereco.Bairro));
        }


        public void Handle(ExcluirEnderecoCommand cmd)
        {
            Endereco endereco = EnderecoExistente(cmd.Id, cmd.MessageType);

            if (endereco == null) return;

            _eventoRepository.RemoverEndereco(cmd.Id);

            if (Commit())
            {
                _mediator.PublicarEvento(new EnderecoEventoExcluidoEvent(cmd.Id));
            }
        }

        public void Handle(AtualizarAtracaoCommand cmd)
        {
            var atracao = AtracaoExistente(cmd.Id, cmd.MessageType);

            if (atracao == null) return;

            atracao.AtualizarAtracao(cmd.Nome, cmd.Estilo);


            if (!atracao.IsValid())
            {
                NotificarValidacoesErro(atracao.ValidationResult);
                return;
            }

            _eventoRepository.AlterarAtracao(atracao);

            if (Commit())
                _mediator.PublicarEvento(new AtracaoAtualizadaEvent(cmd.Id, cmd.Nome, cmd.Estilo));
        }


        public void Handle(AtualizarIngressoCommand cmd)
        {
            var ingresso = IngressoExistente(cmd.Id, cmd.MessageType);

            if (ingresso == null) return;

            ingresso.AtualizarIngresso(cmd.Preco, cmd.Tipo, cmd.Lote);

            if (!ingresso.IsValid())
            {
                NotificarValidacoesErro(ingresso.ValidationResult);
                return;
            }
        }


        public void Handle(AtualizarEventoCommand cmd)
        {
            var evento = EventoExistente(cmd.Id, cmd.MessageType);

            if (evento == null) return;

            evento.AtualizarEvento(cmd.Nome, cmd.Descricao, cmd.SubDescricao, cmd.DescPatrocinadores, cmd.DataHoraInicio, cmd.DataHoraFim, cmd.Situacao, cmd.CategoriaId);

            if (!evento.IsValid())
            {
                NotificarValidacoesErro(evento.ValidationResult);
                return;
            }

            _eventoRepository.Atualizar(evento);

            if (Commit())
                _mediator.PublicarEvento(new EventoAtualizadoEvent(cmd.Id, cmd.Nome, cmd.Descricao, cmd.SubDescricao, cmd.DescPatrocinadores, cmd.DataHoraInicio, cmd.DataHoraFim, cmd.Situacao));

        }

        public void Handle(IncluirIngressoCommand cmd)
        {
            var ingresso = new Ingresso(cmd.Id, cmd.Preco, cmd.Tipo, cmd.Lote, cmd.EventoId);

            if (!ingresso.IsValid())
            {
                NotificarValidacoesErro(ingresso.ValidationResult);
                return;
            }

            _eventoRepository.IncluirIngresso(ingresso);

            if (Commit())
                _mediator.PublicarEvento(new IngressoAdicionadoEvent(ingresso.Id, ingresso.Preco, ingresso.Tipo, ingresso.Lote, ingresso.EventoId));
        }

        public void Handle(IncluirAtracaoCommand cmd)
        {
            var atracao = new Atracao(cmd.Id, cmd.Nome, cmd.Estilo);

            if (!atracao.IsValid())
            {
                NotificarValidacoesErro(atracao.ValidationResult);
                return;
            }

            _eventoRepository.IncluirAtracao(atracao);

            if (Commit())
                _mediator.PublicarEvento(new AtracaoAdicionadaEvent(atracao.Id, atracao.Nome, atracao.Estilo));
        }

        private Ingresso IngressoExistente(Guid id, string messageType)
        {
            var ingresso = _eventoRepository.BuscarIngressoPorId(id);

            if (ingresso != null) return ingresso;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Ingresso não encontrado."));
            return null;
        }


        private Evento EventoExistente(Guid id, string messageType)
        {
            var evento = _eventoRepository.ObterPorId(id);

            if (evento != null) return evento;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Evento não encontrado."));
            return null;
        }


        private Endereco EnderecoExistente(Guid id, string messageType)
        {
            var endereco = _eventoRepository.BuscarEnderecoPorId(id);

            if (endereco != null) return endereco;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Evento não encontrado."));
            return null;
        }


        private Atracao AtracaoExistente(Guid id, string messageType)
        {
            var atracao = _eventoRepository.BuscarAtracaoPorId(id);

            if (atracao != null) return atracao;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Atração não encontrada."));
            return null;
        }


        public void Handle(IncluirEnderecoCommand cmd)
        {
            var cidade = new Cidade(cmd.Cidade.Id,cmd.Cidade.Nome,cmd.Cidade.EstadoId);
            var endereco = new Endereco(cmd.Id, cmd.Logradouro, cmd.Numero, cmd.Complemento, cmd.Cep, cmd.Bairro, cidade);

            if (!endereco.IsValid())
            {
                NotificarValidacoesErro(endereco.ValidationResult);
                return;
            }

            _eventoRepository.IncluirEndereco(endereco);

            if (Commit())
            {
                _mediator.PublicarEvento(new EnderecoEventoAdicionadoEvent(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Cep, endereco.Bairro));
            }
        }



        public void Handle(IncluirEventoCommand cmd)
        {            
            var cidade = new Cidade(cmd.Endereco.Cidade.Id, cmd.Endereco.Cidade.Nome, cmd.Endereco.Cidade.EstadoId);
            var endereco = new Endereco(cmd.Endereco.Id, cmd.Endereco.Logradouro, cmd.Endereco.Numero, cmd.Endereco.Complemento, StringHelper.RemoverCaracteresEspeciais(cmd.Endereco.Cep), cmd.Endereco.Bairro, cidade);
            var evento = new Evento(cmd.Id, cmd.Nome, cmd.Descricao, cmd.SubDescricao, cmd.DescPatrocinadores,
                 cmd.DataHoraInicio, cmd.DataHoraFim, "C", cmd.CategoriaId, cmd.EmpresaId, endereco);

            if (!evento.IsValid())
            {
                NotificarValidacoesErro(evento.ValidationResult);
                return;
            }

            if (cmd.Ingressos == null ||
                cmd.Ingressos.Count() == 0)
            {
                _mediator.PublicarEvento(new DomainNotification(cmd.MessageType, "Ingressos não encontrado."));
                return;
            }

            if (cmd.Atracoes == null ||
                cmd.Atracoes.Count() == 0)
            {
                _mediator.PublicarEvento(new DomainNotification(cmd.MessageType, "Atrações não encontradas."));
                return;
            }

            _eventoRepository.Adicionar(evento);

            foreach (var ingresso in cmd.Ingressos)
            {
                var ing = new Ingresso(ingresso.Id, ingresso.Preco, ingresso.Tipo, ingresso.Lote, evento.Id);

                if (!ing.IsValid())
                {
                    NotificarValidacoesErro(ing.ValidationResult);
                    return;
                }

                _eventoRepository.IncluirIngresso(ing);
            }

            foreach (var atracao in cmd.Atracoes)
            {
                var atracaoObj = new Atracao(atracao.Id, atracao.Nome, atracao.Estilo);

                if (!atracaoObj.IsValid())
                {
                    NotificarValidacoesErro(atracaoObj.ValidationResult);
                    return;
                }
                _eventoRepository.IncluirAtracao(atracaoObj);
                _eventoRepository.IncluirAtracaoEvento(new AtracaoEvento(atracaoObj.Id, evento.Id));
            }

            foreach (var foto in cmd.Fotos)
            {
                Foto fotoBanco = _eventoRepository.BuscarFotoPorId(foto.Id);
                fotoBanco.AtribuirEventoId(evento.Id);
                _eventoRepository.AtualizarFoto(fotoBanco);
            }

            if (Commit())
            {
                _mediator.PublicarEvento(new EventoAdicionadoEvent(evento.Id, evento.Nome, evento.Descricao, evento.SubDescricao, evento.DescPatrocinadores, evento.DataHoraInicio, evento.DataHoraFim, evento.Situacao));
            }
        }

    }
}
