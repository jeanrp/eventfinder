using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class EquipeAdicionadaEvent : Event
    {
        public EquipeAdicionadaEvent(Guid id, string nome, string descricao, Guid empresaId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            EmpresaId = empresaId;
            AggregateId = Id;
        }

        public Guid Id { get;private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Guid EmpresaId { get; private set; }
    }
}
