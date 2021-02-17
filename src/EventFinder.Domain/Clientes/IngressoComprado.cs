using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using FluentValidation;
using System;

namespace EventFinder.Domain.Clientes
{
    public class IngressoComprado : Entity<IngressoComprado>
    {
        public IngressoComprado(DateTime dataHora, int quantidade, string nomeEvento, double valorTotal, Guid clienteId, Guid ingressoId)
        {
            DataHora = dataHora;
            Quantidade = quantidade;
            NomeEvento = nomeEvento;
            ValorTotal = valorTotal;
            ClienteId = clienteId;
            IngressoId = ingressoId;
        }

        protected IngressoComprado() { }

        public DateTime DataHora { get; private set; }
        public int Quantidade { get; private set; }
        public string NomeEvento { get; private set; }
        public double ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid IngressoId { get; private set; }

        public virtual Cliente Cliente { get; private set; }
        public virtual Ingresso Ingresso { get; private set; }

        public void AtribuirClientes(Cliente cliente)
        {
            this.Cliente = cliente;
        }

        public void AtribuirIngressos(Ingresso ingresso)
        {
            this.Ingresso = ingresso;
        }

        public override bool IsValid()
        {
            RuleFor(x => x.DataHora).NotEmpty().WithMessage("A data e a hora são obrigatórias");
            RuleFor(x => x.Quantidade).NotEmpty().WithMessage("A quantidade é obrigatória")
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que 0");

            RuleFor(x => x.NomeEvento).NotEmpty().WithMessage("O nome do evento é obrigatório")
                .Length(100).WithMessage("O nome do evento é obrigatório");

            RuleFor(x => x.ValorTotal).NotEmpty().WithMessage("A quantidade é obrigatória")
                .GreaterThan(0).WithMessage("O valor deve ser maior ou igual a 0"); 

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
