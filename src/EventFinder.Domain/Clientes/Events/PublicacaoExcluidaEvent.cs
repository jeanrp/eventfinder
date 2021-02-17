using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class PublicacaoExcluidaEvent : Event
    {
        public PublicacaoExcluidaEvent(Guid clienteId, Guid eventoId)
        {
            ClienteId = clienteId;
            EventoId = eventoId;
            AggregateId = eventoId;
        }

        public Guid ClienteId { get; private set; }
        public Guid EventoId { get; private set; }
    }
}
