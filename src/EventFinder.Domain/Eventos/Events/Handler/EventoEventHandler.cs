using MediatR;

namespace EventFinder.Domain.Eventos.Events
{
    public class EventoEventHandler :
        INotificationHandler<EventoAdicionadoEvent>,
        INotificationHandler<EventoAtualizadoEvent>,
        INotificationHandler<EventoExcluidoEvent>,
        INotificationHandler<IngressoAdicionadoEvent>,
        INotificationHandler<IngressoAtualizadoEvent>,
        INotificationHandler<IngressoExcluidoEvent>,
        INotificationHandler<EnderecoEventoAdicionadoEvent>,
        INotificationHandler<EnderecoEventoAtualizadoEvent>,
        INotificationHandler<EnderecoEventoExcluidoEvent>,
        INotificationHandler<AtracaoAdicionadaEvent>,
        INotificationHandler<AtracaoAtualizadaEvent>,
        INotificationHandler<AtracaoExcluidaEvent>
    {
        public void Handle(AtracaoExcluidaEvent notification)
        {
        }

        public void Handle(AtracaoAtualizadaEvent notification)
        {
        }

        public void Handle(AtracaoAdicionadaEvent notification)
        {
        }

        public void Handle(EnderecoEventoExcluidoEvent notification)
        {
        }

        public void Handle(EnderecoEventoAtualizadoEvent notification)
        {
        }

        public void Handle(EnderecoEventoAdicionadoEvent notification)
        {
        }

        public void Handle(IngressoExcluidoEvent notification)
        {
        }

        public void Handle(IngressoAtualizadoEvent notification)
        {
        }

        public void Handle(IngressoAdicionadoEvent notification)
        {
        }

        public void Handle(EventoExcluidoEvent notification)
        {
        }

        public void Handle(EventoAtualizadoEvent notification)
        {
        }

        public void Handle(EventoAdicionadoEvent notification)
        {
        }
    }
}
