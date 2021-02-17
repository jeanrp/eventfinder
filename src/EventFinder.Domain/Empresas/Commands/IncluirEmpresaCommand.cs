using EventFinder.Domain.Core.Utils;
using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class IncluirEmpresaCommand : BaseEmpresaCommand
    {
        public IncluirEmpresaCommand(Guid id, string razaoSocial, string email, string telefone, string facebook, string cnpj, Guid? enderecoId, Guid usuarioId)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
            UsuarioId = usuarioId;
        }
    }
}
