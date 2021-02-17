using EventFinder.Domain.Clientes.Events;
using EventFinder.Domain.Clientes.Repository;
using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Core.Utils;
using EventFinder.Domain.Handlers;
using EventFinder.Domain.Interfaces;
using EventFinder.Domain.Usuarios;
using EventFinder.Domain.Usuarios.Repository;
using MediatR;
using System;

namespace EventFinder.Domain.Clientes.Commands.Handler
{
    public class ClienteCommandHandler
        :
        CommandHandler,
        INotificationHandler<AdicionarAvaliacaoCommand>,
        INotificationHandler<AdicionarClienteCommand>,
        INotificationHandler<AdicionarFotoCommand>,
        INotificationHandler<AdicionarIngressoCompradoCommand>,
        INotificationHandler<AdicionarMensagemOrganizacaoEventoCommand>,
        INotificationHandler<AdicionarPublicacaoCommand>,
        INotificationHandler<AtualizarAvaliacaoCommand>,
        INotificationHandler<AtualizarClienteCommand>,
        INotificationHandler<AtualizarPublicacaoCommand>,
        INotificationHandler<ExcluirAvaliacaoCommand>,
        INotificationHandler<ExcluirClienteCommand>,
        INotificationHandler<ExcluirFotoCommand>,
        INotificationHandler<ExcluirPublicacaoCommand>

    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _repository;
        private readonly IMediatorHandler _mediator;

        public ClienteCommandHandler(
            IUsuarioRepository usuarioRepository,
            IClienteRepository repository,
            IUnitOfWork uow,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _repository = repository;
            _mediator = mediator;
        }

        public void Handle(ExcluirPublicacaoCommand cmd)
        {
            Publicacao publicacao = PublicacaoExistente(cmd.ClienteId, cmd.EventoId, cmd.MessageType);

            if (publicacao != null)
            {
                _repository.RemoverPublicacao(publicacao.ClienteId, publicacao.EventoId);

                if (Commit())
                    _mediator.PublicarEvento(new PublicacaoExcluidaEvent(publicacao.ClienteId, publicacao.EventoId));
            }
        }

        public void Handle(ExcluirFotoCommand cmd)
        {
            Foto foto = FotoExistente(cmd.Id, cmd.MessageType);

            if (foto != null)
            {
                _repository.ExcluirFoto(foto.Id);

                if (Commit())
                    _mediator.PublicarEvento(new FotoExcluidaEvent(foto.Id));
            }
        }

        public void Handle(ExcluirClienteCommand cmd)
        {
            Cliente cliente = ClienteExistente(cmd.Id, cmd.MessageType);

            if (cliente != null)
            {
                _repository.ExcluirFoto(cliente.Id);

                if (Commit())
                    _mediator.PublicarEvento(new ClienteExcluidoEvent(cliente.Id));
            }
        }

        public void Handle(ExcluirAvaliacaoCommand cmd)
        {
            Avaliacao avaliacao = AvaliacaoExistente(cmd.ClienteId, cmd.EventoId, cmd.MessageType);

            if (avaliacao != null)
            {
                _repository.RemoverAvaliacao(avaliacao.ClienteId, avaliacao.EventoId);

                if (Commit())
                    _mediator.PublicarEvento(new AvaliacaoExcluidaEvent(avaliacao.EventoId, avaliacao.ClienteId));
            }
        }

        public void Handle(AtualizarPublicacaoCommand cmd)
        {
            Publicacao publicacao = PublicacaoExistente(cmd.ClienteId, cmd.EventoId, cmd.MessageType);

            if (publicacao != null)
            {

                _repository.RemoverPublicacao(publicacao.ClienteId, publicacao.EventoId);

                if (Commit())
                    _mediator.PublicarEvento(new PublicacaoExcluidaEvent(publicacao.ClienteId, publicacao.EventoId));
            }
        }


        public void Handle(AtualizarClienteCommand cmd)
        {
            Cliente cliente = ClienteExistente(cmd.Id, cmd.MessageType);

            if (cliente != null)
            {

                cliente.AtualizarCliente(cmd.Nome, cmd.Email, cmd.Telefone, cmd.Facebook, cmd.DataNascimento, cmd.Sexo, cmd.EstadoCivil, cmd.AtracaoPreferida, cmd.EstiloPreferido);

                if (!cliente.IsValid())
                {
                    NotificarValidacoesErro(cliente.ValidationResult);
                    return;
                }

                Usuario usuario = _usuarioRepository.ObterPorId(cliente.Id);
                usuario.AtualizarUsuario(cmd.Nome, cmd.Email, cmd.Senha);
                _usuarioRepository.Atualizar(usuario);
                _repository.Atualizar(cliente);

                if (Commit())
                    _mediator.PublicarEvento(new ClienteAtualizadoEvent(cliente.Id, cliente.Nome, cliente.Email, cmd.Senha, cliente.Telefone, cliente.Facebook, cliente.DataNascimento, cliente.Sexo, cliente.EstadoCivil, cliente.AtracaoPreferida, cliente.EstiloPreferido));
            }
        }

        public void Handle(AtualizarAvaliacaoCommand cmd)
        {
            Avaliacao avaliacao = AvaliacaoExistente(cmd.ClienteId, cmd.EventoId, cmd.MessageType);

            if (avaliacao != null)
            {

                avaliacao.AtualizarAvaliacao(cmd.Nota, cmd.Descricao);

                if (!avaliacao.IsValid())
                {
                    NotificarValidacoesErro(avaliacao.ValidationResult);
                    return;
                }

                _repository.AtualizarAvaliacao(avaliacao);

                if (Commit())
                    _mediator.PublicarEvento(new AvaliacaoAtualizadaEvent(avaliacao.Nota, avaliacao.Descricao, avaliacao.EventoId, avaliacao.ClienteId));
            }
        }

        public void Handle(AdicionarPublicacaoCommand cmd)
        {
            Publicacao publicacao = new Publicacao(cmd.Descricao, DateTime.Now, cmd.Anexo, cmd.EventoId, cmd.ClienteId);

            if (!publicacao.IsValid())
            {
                NotificarValidacoesErro(publicacao.ValidationResult);
                return;
            }

            _repository.IncluirPublicacao(publicacao);

            if (Commit())
                _mediator.PublicarEvento(new PublicacaoAdicionadaEvent(publicacao.Descricao, publicacao.DataHora, publicacao.Anexo, publicacao.ClienteId, publicacao.EventoId));
        }

        public void Handle(AdicionarMensagemOrganizacaoEventoCommand cmd)
        {
            MensagemOrganizacaoEvento mensagemOrganizacaoEvento = new MensagemOrganizacaoEvento(DateTime.Now, cmd.Descricao, cmd.Anexo, cmd.ClienteId, cmd.EmpresaId);

            if (!mensagemOrganizacaoEvento.IsValid())
            {
                NotificarValidacoesErro(mensagemOrganizacaoEvento.ValidationResult);
                return;
            }

            _repository.EnviarMensagemOrganizacaoEvento(mensagemOrganizacaoEvento);

            if (Commit())
                _mediator.PublicarEvento(new MensagemOrganizacaoEventoAdicionadoEvent(mensagemOrganizacaoEvento.DataHora, mensagemOrganizacaoEvento.Descricao, mensagemOrganizacaoEvento.Anexo, mensagemOrganizacaoEvento.ClienteId, mensagemOrganizacaoEvento.EmpresaId));
        }

        public void Handle(AdicionarIngressoCompradoCommand cmd)
        {
            IngressoComprado iC = new IngressoComprado(DateTime.Now, cmd.Quantidade, cmd.NomeEvento, cmd.ValorTotal, cmd.ClienteId, cmd.IngressoId);

            if (!iC.IsValid())
            {
                NotificarValidacoesErro(iC.ValidationResult);
                return;
            }

            _repository.ComprarIngresso(iC);

            if (Commit())
                _mediator.PublicarEvento(new IngressoCompradoAdicionadoEvent(iC.DataHora, iC.Quantidade, iC.NomeEvento, iC.ValorTotal, iC.ClienteId, iC.IngressoId));
        }

        public void Handle(AdicionarFotoCommand cmd)
        {
            Foto foto = new Foto(cmd.Id, cmd.Descricao, cmd.Imagem, cmd.EventoId, cmd.ClienteId, cmd.EmpresaId);

            if (!foto.IsValid())
            {
                NotificarValidacoesErro(foto.ValidationResult);
                return;
            }

            _repository.IncluirFoto(foto);

            if (Commit())
                _mediator.PublicarEvento(new FotoAdicionadaEvent(foto.Id, foto.Descricao, Convert.ToBase64String(foto.Imagem), foto.EventoId, foto.ClienteId, foto.EmpresaId));
        }

        public void Handle(AdicionarClienteCommand cmd)
        {
            Cliente cliente = new Cliente(cmd.Id, cmd.Nome, cmd.Email, StringHelper.RemoverCaracteresEspeciais(cmd.Telefone), cmd.Facebook, cmd.DataNascimento, cmd.Sexo, cmd.EstadoCivil, cmd.AtracaoPreferida, cmd.EstiloPreferido);
            Usuario usuario = new Usuario(Guid.NewGuid(), cmd.Nome, DateTime.Now, cmd.Email, cmd.Senha, null, null, cliente, cliente.Id);

            if (!usuario.IsValidCliente())
            {
                NotificarValidacoesErro(usuario.ValidationResult);
                return;
            }

            _usuarioRepository.Adicionar(usuario);

            if (Commit())
                _mediator.PublicarEvento(new ClienteAdicionadoEvent(cliente.Id, cliente.Nome, cliente.Email, cliente.Sexo, cliente.Telefone, cliente.Facebook, cliente.DataNascimento, cliente.Sexo, cliente.EstadoCivil, cliente.AtracaoPreferida, cliente.EstiloPreferido));
        }

        public void Handle(AdicionarAvaliacaoCommand cmd)
        {
            Avaliacao avaliacao = new Avaliacao(cmd.Nota, cmd.Descricao, DateTime.Now, cmd.EventoId, cmd.ClienteId);

            if (!avaliacao.IsValid())
            {
                NotificarValidacoesErro(avaliacao.ValidationResult);
                return;
            }

            _repository.IncluirAvaliacao(avaliacao);

            if (Commit())
                _mediator.PublicarEvento(new AvaliacaoAdicionadaEvent(avaliacao.Nota, avaliacao.Descricao, avaliacao.DataHora, avaliacao.EventoId, avaliacao.ClienteId));
        }

        public Avaliacao AvaliacaoExistente(Guid clienteId, Guid eventoId, string messageType)
        {
            Avaliacao avaliacao = _repository.BuscarAvaliacaoPorId(clienteId, eventoId);

            if (avaliacao != null) return avaliacao;


            _mediator.PublicarEvento(new DomainNotification(messageType, "A avaliacao não foi encontrada"));
            return null;
        }

        public Cliente ClienteExistente(Guid clienteId, string messageType)
        {
            Cliente cliente = _repository.ObterPorId(clienteId);

            if (cliente != null) return cliente;

            _mediator.PublicarEvento(new DomainNotification(messageType, "O cliente não foi encontrado"));
            return null;
        }

        public Foto FotoExistente(Guid fotoId, string messageType)
        {
            Foto foto = _repository.BuscarFotoPorId(fotoId);

            if (foto != null) return foto;

            _mediator.PublicarEvento(new DomainNotification(messageType, "A foto não foi encontrada"));
            return null;
        }

        public IngressoComprado IngressoCompradoExistente(Guid clienteId, Guid ingressoId, string messageType)
        {
            IngressoComprado ingressoComprado = _repository.BuscarIngressoCompradoPorId(clienteId, ingressoId);

            if (ingressoComprado != null) return ingressoComprado;

            _mediator.PublicarEvento(new DomainNotification(messageType, "O ingresso comprado não foi encontrado"));
            return null;
        }

        public Publicacao PublicacaoExistente(Guid clienteId, Guid eventoId, string messageType)
        {
            Publicacao publicacao = _repository.BuscarPublicacaoPorId(clienteId, eventoId);

            if (publicacao != null) return publicacao;

            _mediator.PublicarEvento(new DomainNotification(messageType, "A publicação não foi encontrada"));
            return null;
        }


    }
}
