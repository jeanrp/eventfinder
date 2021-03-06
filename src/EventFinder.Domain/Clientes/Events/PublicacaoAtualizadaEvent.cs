﻿using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class PublicacaoAtualizadaEvent : Event
    {
        public PublicacaoAtualizadaEvent(string descricao, DateTime dataHora, byte[] anexo, Guid clienteId, Guid eventoId)
        {
            Descricao = descricao;
            DataHora = dataHora;
            Anexo = anexo;
            ClienteId = clienteId;
            EventoId = eventoId;
        }

        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }
        public byte[] Anexo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EventoId { get; private set; }
    }
}
