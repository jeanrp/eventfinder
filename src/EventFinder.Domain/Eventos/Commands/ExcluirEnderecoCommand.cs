using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class ExcluirEnderecoCommand : Command
    {
        public ExcluirEnderecoCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }


        public Guid Id { get; set; }
    }
}
