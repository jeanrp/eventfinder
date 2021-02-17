using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Eventos.Events
{
    public class AtracaoExcluidaEvent : Event
    {
        public AtracaoExcluidaEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
