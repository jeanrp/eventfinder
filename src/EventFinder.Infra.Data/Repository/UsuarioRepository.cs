using System;
using System.Collections.Generic;
using EventFinder.Domain.Usuarios;
using EventFinder.Domain.Usuarios.Repository;
using EventFinder.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventFinder.Infra.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EventosContext context) : base(context)
        {
        }

        public IEnumerable<PermissaoUsuarioFuncao> BuscarPermissoesPorUsuario(Guid usuarioId)
        {
            throw new Exception();
        }

        public Usuario BuscarPorEmailSenha(string email, string senha)
        {
            return DbSet.AsNoTracking().Include(x => x.Empresa).Include(x => x.Cliente).FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }
    }
}
