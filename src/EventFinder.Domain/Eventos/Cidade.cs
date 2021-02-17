using EventFinder.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Eventos
{
    public class Cidade : Entity<Cidade>
    {

        protected Cidade() { }

        public Cidade(Guid id,string nome, Guid estadoId)
        {
            Id = id;
            Nome = nome;
            EstadoId = estadoId;
        }

        public string Nome { get; private set; }
        public Guid EstadoId { get; private set; }

        public virtual Estado Estado { get;private set; }

        public IEnumerable<Endereco> Enderecos => _enderecos;
        private readonly List<Endereco> _enderecos = new List<Endereco>();

        public void AtribuirEstado(Estado estado)
        {
            this.Estado = estado;
        }

        public override bool IsValid()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da cidade é requerido")
                .Length(2,100).WithMessage("O nome da cidade deve ter no mínimo 2 caracteres e no máximo 100.");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
