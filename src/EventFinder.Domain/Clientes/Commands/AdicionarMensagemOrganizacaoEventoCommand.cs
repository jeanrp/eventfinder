using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class AdicionarMensagemOrganizacaoEventoCommand : Command
    {
        public AdicionarMensagemOrganizacaoEventoCommand(string descricao, byte[] anexo, Guid clienteId, Guid empresaId)
        {
            Descricao = descricao;
            Anexo = anexo;
            ClienteId = clienteId;
            EmpresaId = empresaId;
        }
        
        public string Descricao { get; private set; }
        public byte[] Anexo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EmpresaId { get; private set; }
    }
}
