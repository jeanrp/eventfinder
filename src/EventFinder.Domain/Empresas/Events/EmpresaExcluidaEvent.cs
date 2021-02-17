using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class EmpresaExcluidaEvent : BaseEmpresaEvent
    {
        public EmpresaExcluidaEvent(Guid id)
        {
            this.Id = id;
            this.AggregateId = Id;
        }
    }
}
