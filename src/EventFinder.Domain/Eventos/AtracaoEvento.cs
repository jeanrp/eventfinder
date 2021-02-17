using EventFinder.Domain.Core.Models;
using System;

namespace EventFinder.Domain.Eventos
{
    public class AtracaoEvento : Entity<Atracao>
    {
        protected AtracaoEvento() { }

        public AtracaoEvento(Guid atracaoId, Guid eventoId)
        {
            AtracaoId = atracaoId;
            EventoId = eventoId;
        }

        public Guid AtracaoId { get;private set; }
        public Guid EventoId { get; private set; }
        public virtual Atracao Atracao { get; private set; }
        public virtual Evento Evento { get; private set; }

        public void AtribuirAtracao(Atracao atracao)
        {
            this.Atracao = atracao;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
