using System;

namespace EventFinder.Domain.Eventos.Events
{
    public class EventoAtualizadoEvent : BaseEventoEvent
    {
        public EventoAtualizadoEvent(Guid id, string nome, string descricao, string subDescricao, string descPatrocinadores, DateTime dataHoraInicio, DateTime dataHoraFim, string situacao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            SubDescricao = subDescricao;
            DescPatrocinadores = descPatrocinadores;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Situacao = situacao;
        }
    }
}
