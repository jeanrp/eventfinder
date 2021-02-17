using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class ExcluirPublicacaoCommand : Command
    {
        public ExcluirPublicacaoCommand(Guid clienteId, Guid eventoId)
        {
            ClienteId = clienteId;
            EventoId = eventoId;
            AggregateId = eventoId;
        }

        public Guid ClienteId { get; private set; }
        public Guid EventoId { get; private set; }
    }
}

