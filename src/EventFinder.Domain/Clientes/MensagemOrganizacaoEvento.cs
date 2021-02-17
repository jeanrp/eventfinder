using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Empresas;
using FluentValidation;
using System;

namespace EventFinder.Domain.Clientes
{
    public class MensagemOrganizacaoEvento : Entity<MensagemOrganizacaoEvento>
    {

        protected MensagemOrganizacaoEvento() {  }
        public MensagemOrganizacaoEvento(DateTime dataHora, string descricao, byte[] anexo, Guid clienteId, Guid empresaId)
        {
            DataHora = dataHora;
            Descricao = descricao;
            Anexo = anexo;
            ClienteId = clienteId;
            EmpresaId = empresaId;
        }

        public DateTime DataHora { get; private set; }
        public string Descricao { get; private set; }
        public byte[] Anexo { get;private set; }
        public Guid ClienteId { get;private set; }
        public Guid EmpresaId { get;private set; }
        public virtual Cliente Cliente { get;private set; }
        public virtual Empresa Empresa { get; private set; }

        public override bool IsValid()
        {
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição é obrigatória");
            RuleFor(x => x.Anexo).NotEmpty().WithMessage("O anexo é obrigatório");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
