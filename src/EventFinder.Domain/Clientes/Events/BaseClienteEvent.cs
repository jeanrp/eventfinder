using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Clientes.Events
{
    public class BaseClienteEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Senha { get; protected set; }
        public string Email { get; protected set; }
        public string Telefone { get; protected set; }
        public string Facebook { get; protected set; }
        public DateTime DataNascimento { get; protected set; }
        public string Sexo { get; protected set; }
        public string EstadoCivil { get; protected set; }
        public string AtracaoPreferida { get; protected set; }
        public string EstiloPreferido { get; protected set; }
        public Guid UsuarioId { get; set; }
    }
}
