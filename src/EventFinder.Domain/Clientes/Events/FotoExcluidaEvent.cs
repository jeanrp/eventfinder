using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class FotoExcluidaEvent : Event
    {
        public FotoExcluidaEvent(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }

        public Guid Id { get; private set; }
    }
}
