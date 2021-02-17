using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class PublicacaoAdicionadaEvent : Event
    {
        public PublicacaoAdicionadaEvent(string descricao, DateTime dataHora, byte[] anexo, Guid clienteId, Guid eventoId)
        {
            Descricao = descricao;
            DataHora = dataHora;
            Anexo = anexo;
            ClienteId = clienteId;
            EventoId = eventoId;
            AggregateId = EventoId;
        }

        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }
        public byte[] Anexo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EventoId { get; private set; }
    }
}
