using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Eventos.Events
{
    public abstract class BaseEventoEvent : Event
    {

        public Guid Id { get;protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string SubDescricao { get; protected set; }
        public string DescPatrocinadores { get; protected set; }
        public DateTime DataHoraInicio { get; protected set; }
        public DateTime DataHoraFim { get; protected set; }
        public string Situacao { get; protected set; }
 
    }
}
