using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class IncluirCidadeCommand : Command
    {
        public IncluirCidadeCommand(Guid id, string nome, Guid estadoId)
        {
            Id = id;
            Nome = nome;
            EstadoId = estadoId;
        }

        public Guid Id { get;private set; }
        public string Nome { get; private set; }
        public Guid EstadoId { get; private set; }
    }
}
