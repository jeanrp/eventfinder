using EventFinder.Domain.Clientes;
using EventFinder.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Eventos
{
    public class Ingresso : Entity<Ingresso>
    {
        protected Ingresso()
        {
         }

        public Ingresso(Guid id,decimal preco, string tipo, short lote, Guid eventoId)
        {
            Id = id;
            Preco = preco;
            Tipo = tipo;
            Lote = lote;
            EventoId = eventoId;
        }

        public decimal Preco { get;private set; }
        public string Tipo { get; private set; }
        public short Lote { get;private set; }
        public Guid EventoId { get; private set; }
        public virtual Evento Evento { get; private set; }

        public IEnumerable<IngressoComprado> IngressosComprados => _ingressosComprados;  
        private readonly List<IngressoComprado> _ingressosComprados = new List<IngressoComprado>();

        public void AdicionarIngressosComprados(IngressoComprado ic)
        {
            _ingressosComprados.Add(ic);
        }
        
        public void AtualizarIngresso(decimal preco, string tipo, short lote)
        {
            this.Preco = preco;
            this.Tipo = tipo;
            this.Lote = lote;
        }

        public override bool IsValid()
        {
            RuleFor(c => c.Preco)
                .NotEmpty().WithMessage("O preco precisa ser fornecido")
                .GreaterThan(0).WithMessage("O preço tem que ser maior que 0");
                       
            RuleFor(c => c.Tipo)
                         .NotEmpty().WithMessage("O tipo precisa ser fornecido")
                         .Length(2, 50).WithMessage("O tipo precisa ter entre 2 e 50 caracteres");

            RuleFor(c => c.Lote)
                .GreaterThan(Convert.ToInt16(0)).WithMessage("O lote tem que ser maior que 0");

            RuleFor(c => c.EventoId)
           .NotNull().WithMessage("O evento precisa ser fornecido");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
