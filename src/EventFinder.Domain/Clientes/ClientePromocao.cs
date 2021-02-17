using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using System;

namespace EventFinder.Domain.Clientes
{
    public class ClientePromocao : Entity<ClientePromocao>
    {
        protected ClientePromocao() { }

        public ClientePromocao(Guid clienteId, Guid promocaoId)
        {
            ClienteId = clienteId;
            PromocaoId = promocaoId;
        }

        public Guid ClienteId { get; set; }
        public Guid PromocaoId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Promocao Promocao { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

    }
}
