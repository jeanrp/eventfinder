using EventFinder.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Eventos
{
    public class Estado : Entity<Estado>
    {
        public Estado() {  }

        public Estado(Guid id,string uf)
        {
            Id = id;
            Uf = uf;
        }

        public string Uf { get; private set; }

        public IEnumerable<Cidade> Cidades  => _cidades;
        private readonly List<Cidade> _cidades = new List<Cidade>();

        public void AdicionarCidades(Cidade cidade)
        {
            _cidades.Add(cidade);
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
