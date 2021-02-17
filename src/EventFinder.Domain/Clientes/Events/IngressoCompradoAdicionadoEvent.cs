using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class IngressoCompradoAdicionadoEvent : Event
    {
        public IngressoCompradoAdicionadoEvent(DateTime dataHora, int quantidade, string nomeEvento, double valorTotal, Guid clienteId, Guid ingressoId)
        {
            DataHora = dataHora;
            Quantidade = quantidade;
            NomeEvento = nomeEvento;
            ValorTotal = valorTotal;
            ClienteId = clienteId;
            IngressoId = ingressoId;
            AggregateId = ClienteId;
        }

        public DateTime DataHora { get; private set; }
        public int Quantidade { get; private set; }
        public string NomeEvento { get; private set; }
        public double ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid IngressoId { get; private set; }
    }
}
