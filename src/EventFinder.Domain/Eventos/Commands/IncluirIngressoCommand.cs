using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class IncluirIngressoCommand : Command
    {
        public IncluirIngressoCommand(Guid id, decimal preco, string tipo, short lote, Guid eventoId)
        {
            Id = id;
            Preco = preco;
            Tipo = tipo;
            Lote = lote;
            EventoId = eventoId;
        }

        public Guid Id { get; private set; }
        public decimal Preco { get; private set; }
        public string Tipo { get; private set; }
        public short Lote { get; private set; }
        public Guid EventoId { get; private set; }
    }
}
