using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class AvaliacaoAtualizadaEvent : Event
    {
        public AvaliacaoAtualizadaEvent(decimal nota, string descricao, Guid eventoId, Guid clienteId)
        {
            Nota = nota;
            Descricao = descricao;
            EventoId = eventoId;
            ClienteId = clienteId;
            AggregateId = EventoId;
        }

        public decimal Nota { get; private set; }
        public string Descricao { get; private set; } 
        public Guid EventoId { get; private set; }
        public Guid ClienteId { get; private set; }
    }
}
