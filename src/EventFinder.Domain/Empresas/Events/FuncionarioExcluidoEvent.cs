using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class FuncionarioExcluidoEvent : Event
    {
        public FuncionarioExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }

        public Guid Id { get; private set; }
    }
}
