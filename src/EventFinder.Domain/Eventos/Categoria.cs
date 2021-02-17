using EventFinder.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Eventos
{
    public class Categoria : Entity<Categoria>
    {
        protected Categoria() {  }

        public Categoria(Guid id,string descricao, short classificacao)
        {
            Id = id;
            Descricao = descricao;
            Classificacao = classificacao;
        }

        public string Descricao { get;private set; }
        public short Classificacao { get; private set; }

        public IEnumerable<Evento> Eventos => _eventos; 
        private readonly List<Evento> _eventos = new List<Evento>();

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
