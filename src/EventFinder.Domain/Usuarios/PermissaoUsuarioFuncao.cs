using EventFinder.Domain.Core.Models;
using System;

namespace EventFinder.Domain.Usuarios
{
    public class PermissaoUsuarioFuncao : Entity<PermissaoUsuarioFuncao>
    {
        public PermissaoUsuarioFuncao(Guid id,string permissoes, Guid? funcaoId, Guid? usuarioId)
        {
            Id = id;
            Permissoes = permissoes;
            FuncaoId = funcaoId;
            UsuarioId = usuarioId;
        }

        public string Permissoes { get;private set; }
        public Guid? FuncaoId { get;private set; }
        public Guid? UsuarioId { get;private set; }

        public virtual Funcao Funcao { get;private set; }
        public virtual Usuario Usuario { get;private set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
