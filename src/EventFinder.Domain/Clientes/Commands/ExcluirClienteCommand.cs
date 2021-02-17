using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class ExcluirClienteCommand : BaseClienteCommand
    {
        public ExcluirClienteCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}
