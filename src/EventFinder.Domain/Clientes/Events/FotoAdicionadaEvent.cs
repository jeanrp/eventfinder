using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class FotoAdicionadaEvent : Event
    {
        public FotoAdicionadaEvent(Guid id, string descricao, string imagem, Guid? eventoId, Guid? clienteId, Guid? empresaId)
        {
            Id = id;
            Descricao = descricao;
            Imagem = imagem;
            EventoId = eventoId;
            ClienteId = clienteId;
            EmpresaId = empresaId;
            AggregateId = Id;
        }

        public Guid Id { get; set; }
        public string Descricao { get; private set; }
        public string Imagem { get; private set; }
        public Guid? EventoId { get; private set; }
        public Guid? ClienteId { get; private set; }
        public Guid? EmpresaId { get; private set; }
    }
}
