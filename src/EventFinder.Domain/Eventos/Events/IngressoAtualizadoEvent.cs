using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Eventos.Events
{
    public class IngressoAtualizadoEvent : Event
    {
        public IngressoAtualizadoEvent(Guid id, decimal preco, string tipo, short lote, Guid eventoId)
        {
            Id = id;
            Preco = preco;
            Tipo = tipo;
            Lote = lote;
            AggregateId = eventoId;
        }
        public Guid Id { get; private set; }
        public decimal Preco { get; private set; }
        public string Tipo { get; private set; }
        public short Lote { get; private set; }
    }
}
