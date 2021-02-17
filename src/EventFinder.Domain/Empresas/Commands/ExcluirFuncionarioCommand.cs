using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class ExcluirFuncionarioCommand : Command
    {
        public ExcluirFuncionarioCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }

        public Guid Id { get;private set; }
    }
}
