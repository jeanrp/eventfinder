using EventFinder.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventFinder.Domain.Eventos
{
    public class Atracao : Entity<Atracao>
    {
        protected Atracao() { }

        public Atracao(Guid id, string nome, string estilo)
        {
            Id = id;
            Nome = nome;
            Estilo = estilo;
        }

        public string Nome { get; private set; }

        public string Estilo { get; private set; }

        public IEnumerable<AtracaoEvento> AtracoesEventos => _atracoesEventos;

        private readonly List<AtracaoEvento> _atracoesEventos = new List<AtracaoEvento>();

        public void AtualizarAtracao(string nome, string estilo)
        {
            this.Nome = nome;
            this.Estilo = estilo;
        }

        public void AtribuirAtracaoId(Guid id)
        {
            this.Id = id;
        }

        public override bool IsValid()
        {
            RuleFor(c => c.Nome)
                        .NotEmpty().WithMessage("O nome precisa ser fornecido")
                        .Length(2, 150).WithMessage("O nome precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Estilo)
                         .NotEmpty().WithMessage("O estilo precisa ser fornecido")
                         .Length(2, 100).WithMessage("O estilo precisa ter entre 2 e 100 caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;

        }
    }
}
