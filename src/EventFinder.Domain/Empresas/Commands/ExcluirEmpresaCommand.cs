using System;
using System.Collections.Generic;
using System.Text;

namespace EventFinder.Domain.Empresas.Commands
{
    public class ExcluirEmpresaCommand : BaseEmpresaCommand
    {
        public ExcluirEmpresaCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }        
    }
}
