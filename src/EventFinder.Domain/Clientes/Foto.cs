using System;
using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Empresas;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFinder.Domain.Clientes
{
    public class Foto : Entity<Foto>
    {

        protected Foto() { }

        public Foto(Guid id, string descricao, byte[] imagem, Guid? eventoId, Guid? clienteId, Guid? empresaId)
        {
            Id = id;
            Descricao = descricao;
            Imagem = imagem;
            EventoId = eventoId;
            ClienteId = clienteId;
            EmpresaId = empresaId;
        }

        public string Descricao { get; private set; }
        public byte[] Imagem { get; private set; }
        public Guid? EventoId { get; private set; }
        public Guid? ClienteId { get; private set; }
        public Guid? EmpresaId { get; private set; }
        public virtual Evento Evento { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual Empresa Empresa { get; private set; }
        [NotMapped]
        public string ImagemBase64 { get;private set; }

        public void AtribuirEventoId(Guid? eventoId)
        {
            this.EventoId = eventoId;
        }

        public void AtribuirClienteId(Guid? clienteId)
        {
            this.ClienteId = clienteId;
        }

        public void AtribuirEmpresaId(Guid? empresaId)
        {
            this.EmpresaId = empresaId;
        }

        public void AtribuirImagemBase64()
        {
            this.ImagemBase64 = Convert.ToBase64String(this.Imagem);
        }


        public override bool IsValid()
        {
            RuleFor(x => x.Imagem).NotEmpty().WithMessage("A imagem é obrigatória");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
