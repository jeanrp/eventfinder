using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class AvaliacaoExcluidaEvent : Event
    {
        public AvaliacaoExcluidaEvent(Guid eventoId, Guid clienteId)
        {
            EventoId = eventoId;
            ClienteId = clienteId;
            AggregateId = eventoId;
        }

        public Guid EventoId { get; private set; }
        public Guid ClienteId { get; private set; }
    }
}
