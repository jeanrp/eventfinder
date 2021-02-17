using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class AdicionarAvaliacaoCommand : Command
    {
        public AdicionarAvaliacaoCommand(decimal nota, string descricao,Guid eventoId, Guid clienteId)
        {
            Nota = nota;
            Descricao = descricao;
            EventoId = eventoId;
            ClienteId = clienteId;
        }

        public decimal Nota { get; private set; }
        public string Descricao { get; private set; }
        public Guid EventoId { get; private set; }
        public Guid ClienteId { get; private set; }
    }
}
