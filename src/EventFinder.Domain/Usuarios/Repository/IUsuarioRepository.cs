using EventFinder.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Usuarios.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        IEnumerable<PermissaoUsuarioFuncao> BuscarPermissoesPorUsuario(Guid usuarioId);

        Usuario BuscarPorEmailSenha(string email, string senha);
    }
}
