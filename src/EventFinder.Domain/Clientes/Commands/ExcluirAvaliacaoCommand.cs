using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class ExcluirAvaliacaoCommand : Command
    {
        public ExcluirAvaliacaoCommand(Guid eventoId, Guid clienteId)
        {
            EventoId = eventoId;
            ClienteId = clienteId;
            AggregateId = eventoId;
        }

        public Guid EventoId { get; private set; }
        public Guid ClienteId { get; private set; }

    }
}
