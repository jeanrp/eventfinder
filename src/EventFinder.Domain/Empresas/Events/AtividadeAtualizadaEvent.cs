using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class AtividadeAtualizadaEvent : Event
    {
        public AtividadeAtualizadaEvent(Guid id, string descricao, string nome, DateTime dataHoraInicio, DateTime dataHoraFim, Guid? funcionarioId)
        {
            Id = id;
            Descricao = descricao;
            Nome = nome;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            FuncionarioId = funcionarioId;
            AggregateId = Id;
        }

        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public Guid? FuncionarioId { get; private set; }
    }
}
