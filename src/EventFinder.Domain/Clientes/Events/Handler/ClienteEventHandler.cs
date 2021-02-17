using MediatR;
using System;

namespace EventFinder.Domain.Clientes.Events.Handler
{
    public class ClienteEventHandler :
        INotificationHandler<AvaliacaoAdicionadaEvent>,
        INotificationHandler<AvaliacaoAtualizadaEvent>,
        INotificationHandler<AvaliacaoExcluidaEvent>,
        INotificationHandler<ClienteAdicionadoEvent>,
        INotificationHandler<ClienteAtualizadoEvent>,
        INotificationHandler<ClienteExcluidoEvent>,
        INotificationHandler<FotoAdicionadaEvent>,
        INotificationHandler<FotoExcluidaEvent>,
        INotificationHandler<IngressoCompradoAdicionadoEvent>,
        INotificationHandler<MensagemOrganizacaoEventoAdicionadoEvent>,
        INotificationHandler<PublicacaoAdicionadaEvent>,
        INotificationHandler<PublicacaoAtualizadaEvent>,
        INotificationHandler<PublicacaoExcluidaEvent>

    {
       
        public void Handle(PublicacaoExcluidaEvent notification)
        {
        }

        public void Handle(PublicacaoAtualizadaEvent notification)
        {
        }

        public void Handle(PublicacaoAdicionadaEvent notification)
        {
        }

        public void Handle(MensagemOrganizacaoEventoAdicionadoEvent notification)
        {
        }

        public void Handle(IngressoCompradoAdicionadoEvent notification)
        {
        }

        public void Handle(FotoExcluidaEvent notification)
        {
        }

        public void Handle(FotoAdicionadaEvent notification)
        {
        }

        public void Handle(ClienteExcluidoEvent notification)
        {
        }

        public void Handle(ClienteAtualizadoEvent notification)
        {
        }

        public void Handle(ClienteAdicionadoEvent notification)
        {
        }

        public void Handle(AvaliacaoExcluidaEvent notification)
        {
        }

        public void Handle(AvaliacaoAtualizadaEvent notification)
        {
        }

        public void Handle(AvaliacaoAdicionadaEvent notification)
        {
        }
    }
}
