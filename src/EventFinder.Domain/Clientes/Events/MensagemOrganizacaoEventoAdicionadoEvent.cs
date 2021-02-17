using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class MensagemOrganizacaoEventoAdicionadoEvent : Event
    {

        public MensagemOrganizacaoEventoAdicionadoEvent(DateTime dataHora, string descricao, byte[] anexo, Guid clienteId, Guid empresaId)
        {
            DataHora = dataHora;
            Descricao = descricao;
            Anexo = anexo;
            ClienteId = clienteId;
            EmpresaId = empresaId;
            AggregateId = ClienteId;
        }

        public DateTime DataHora { get; private set; }
        public string Descricao { get; private set; }
        public byte[] Anexo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EmpresaId { get; private set; }
    }
}
