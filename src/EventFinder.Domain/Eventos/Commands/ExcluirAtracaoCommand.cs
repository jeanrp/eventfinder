using EventFinder.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventFinder.Domain.Eventos.Commands
{
    public class ExcluirAtracaoCommand : Command
    {
        public ExcluirAtracaoCommand(Guid id, Guid eventoId)
        {
            Id = id;
            this.EventoId = eventoId;
            AggregateId = eventoId;
        }

        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
    }
}
