using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class EditarEmpresaCommand : BaseEmpresaCommand
    {
        public EditarEmpresaCommand(Guid id, string razaoSocial, string email, string telefone, string facebook, string cnpj)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cnpj = cnpj;
        }
    }
}
