using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class EmpresaAtualizadaEvent : BaseEmpresaEvent
    {
        public EmpresaAtualizadaEvent(Guid id, string razaoSocial, string email, string telefone, string facebook, string cnpj)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cnpj = cnpj;
            AggregateId = Id;
        }
    }
}
