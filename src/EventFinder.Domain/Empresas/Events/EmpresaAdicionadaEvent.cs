using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class EmpresaAdicionadaEvent : BaseEmpresaEvent
    {

        public EmpresaAdicionadaEvent(Guid id, string razaoSocial, string email, string telefone, string facebook, string cnpj, Guid? enderecoId)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
            AggregateId = Id;
        }

    }
}
