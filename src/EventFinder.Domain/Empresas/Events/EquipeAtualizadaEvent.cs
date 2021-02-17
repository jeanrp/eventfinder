using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class EquipeAtualizadaEvent : Event
    {
        public EquipeAtualizadaEvent(Guid id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
    }
}
