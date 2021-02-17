using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class AdicionarIngressoCompradoCommand : Command
    {
        public AdicionarIngressoCompradoCommand(int quantidade, string nomeEvento, double valorTotal, Guid clienteId, Guid ingressoId)
        { 
            Quantidade = quantidade;
            NomeEvento = nomeEvento;
            ValorTotal = valorTotal;
            ClienteId = clienteId;
            IngressoId = ingressoId;
        }
        
        public int Quantidade { get; private set; }
        public string NomeEvento { get; private set; }
        public double ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid IngressoId { get; private set; }
    }
}
