using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Eventos.Events
{
    public class AtracaoAtualizadaEvent : Event
    {
        public AtracaoAtualizadaEvent(Guid id, string nome, string estilo)
        {
            Id = id;
            Nome = nome;
            Estilo = estilo;
        }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }

        public string Estilo { get; private set; }
    }
}
