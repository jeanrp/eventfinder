using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public abstract class BaseEmpresaEvent : Event
    {


        public Guid Id { get;protected set; }
        public string RazaoSocial { get; protected set; }
        public string Email { get; protected set; }
        public string Telefone { get; protected set; }
        public string Facebook { get; protected set; }
        public string Cnpj { get; protected set; }
        public Guid? EnderecoId { get; protected set; }     
        public Guid UsuarioId { get; protected set; } 
    }
}
