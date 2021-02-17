using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class ClienteExcluidoEvent : BaseClienteEvent
    {
        public ClienteExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}
