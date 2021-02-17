using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class AvaliacaoAdicionadaEvent : Event
    {
        public AvaliacaoAdicionadaEvent(decimal nota, string descricao, DateTime dataHora, Guid eventoId, Guid clienteId)
        {
            Nota = nota;
            Descricao = descricao;
            DataHora = dataHora;
            EventoId = eventoId;
            ClienteId = clienteId;
            AggregateId = EventoId;
        }

        public decimal Nota { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }
        public Guid EventoId { get; private set; }
        public Guid ClienteId { get; private set; }
    }
}
