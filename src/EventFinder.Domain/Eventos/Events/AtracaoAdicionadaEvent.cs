using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Eventos.Events
{
    public class AtracaoAdicionadaEvent : Event
    {
        public AtracaoAdicionadaEvent(Guid id, string nome, string estilo)
        {
            Id = id;
            Nome = nome;
            Estilo = estilo;
        }

        public Guid Id { get;private set; }

        public string Nome { get; private set; }

        public string Estilo { get; private set; }

    }
}
