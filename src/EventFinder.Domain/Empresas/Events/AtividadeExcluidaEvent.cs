using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class AtividadeExcluidaEvent : Event
    {
        public AtividadeExcluidaEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get;private set; }


    }
}
