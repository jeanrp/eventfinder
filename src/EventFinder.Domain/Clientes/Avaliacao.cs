using System;
using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using FluentValidation;

namespace EventFinder.Domain.Clientes
{
    public class Avaliacao : Entity<Avaliacao>
    {
        protected Avaliacao() { }
 
        public Avaliacao(decimal nota, string descricao, DateTime dataHora, Guid eventoId, Guid clienteId)
        {
            Nota = nota;
            Descricao = descricao;
            DataHora = dataHora;
            EventoId = eventoId;
            ClienteId = clienteId;
        }


        public decimal Nota { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }
        public Guid EventoId { get; private set; }
        public Guid ClienteId { get; private set; }

        public virtual Evento Evento { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        

        public override bool IsValid()
        {
            RuleFor(x => x.Nota).NotEmpty().WithMessage("A nota é obrigatória")
                .LessThan(Convert.ToDecimal(10.01)).WithMessage("A nota deve ter no máximo a nota 10.")
                .GreaterThanOrEqualTo(0).WithMessage("A nota deve ser maior que 0");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição é obrigatória")
                .Length(2,500).WithMessage("A descrição deve ter até 500 caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public void AtualizarAvaliacao(decimal nota, string descricao)
        {
            this.Nota = nota;
            this.Descricao = descricao;
        }
    }
}
