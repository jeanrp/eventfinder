using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class ExcluirIngressoCommand : Command
    {
        public ExcluirIngressoCommand(Guid id)
        {
            this.Id = id;
            this.AggregateId = Id;
        }

        public Guid Id { get; set; }
    }
}
