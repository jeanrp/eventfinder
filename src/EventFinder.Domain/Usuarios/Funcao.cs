using EventFinder.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Usuarios
{
    public class Funcao : Entity<Funcao>
    {
        protected Funcao() { }

        public Funcao(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public string Descricao { get; private set; }

        public IEnumerable<PermissaoUsuarioFuncao> Permissoes => _permissoes; 

        private readonly List<PermissaoUsuarioFuncao> _permissoes = new List<PermissaoUsuarioFuncao>();

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
