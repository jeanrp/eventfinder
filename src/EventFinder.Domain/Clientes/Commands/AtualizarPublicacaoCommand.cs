using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class AtualizarPublicacaoCommand: Command
    {
        public AtualizarPublicacaoCommand(string descricao, byte[] anexo, Guid clienteId, Guid eventoId)
        {
            Descricao = descricao;            
            Anexo = anexo;
            ClienteId = clienteId;
            EventoId = eventoId;
        }

        public string Descricao { get; private set; }        
        public byte[] Anexo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EventoId { get; private set; }
    }
}
