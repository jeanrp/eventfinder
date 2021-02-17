using System;
using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using FluentValidation;

namespace EventFinder.Domain.Clientes
{
    public class Publicacao : Entity<Publicacao>
    {

        protected Publicacao() { }

        public Publicacao(string descricao, DateTime dataHora, byte[] anexo, Guid clienteId, Guid eventoId)
        {
            Descricao = descricao;
            DataHora = dataHora;
            Anexo = anexo;
            ClienteId = clienteId;
            EventoId = eventoId;
        }
    
        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }
        public byte[] Anexo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EventoId { get; private set; }

        public virtual Cliente Cliente { get;private set; }
        public virtual Evento Evento { get;private set; }

        public void AtualizarPublicacao(string descricao, byte[] anexo)
        {
            this.Descricao = descricao;
            this.Anexo = anexo;
        }

        public override bool IsValid()
        {
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição é obrigatória");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
