using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class ExcluirFotoCommand : Command
    {
        public ExcluirFotoCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }

        public Guid Id { get;private set; }
    }
}
