using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class ExcluirEquipeCommand : Command
    {
        public ExcluirEquipeCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }

        public Guid Id { get; private set; }
    }
}
