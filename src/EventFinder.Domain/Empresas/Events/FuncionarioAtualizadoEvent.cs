using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Empresas.Events
{
    public class FuncionarioAtualizadoEvent : Event
    {
        public FuncionarioAtualizadoEvent(Guid id, string nome, string email, string telefone, string facebook, string cargo, string sexo, Guid equipeId)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cargo = cargo;
            Sexo = sexo;
            EquipeId = equipeId;
            AggregateId = EquipeId;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Facebook { get; private set; }
        public string Cargo { get; private set; }
        public string Sexo { get; private set; }        
        public Guid EquipeId { get; private set; }

    }
}
