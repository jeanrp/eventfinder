using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Empresas;
using FluentValidation;
using System;

namespace EventFinder.Domain.Funcionarios
{
    public class Atividade : Entity<Atividade>
    {
        protected Atividade() {  }

 
        public Atividade(Guid id,string descricao, string nome, DateTime dataHoraInicio, DateTime dataHoraFim, Guid empresaId)
        {
            Id = id;
            Descricao = descricao;
            Nome = nome;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            EmpresaId = empresaId;
        }


        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get;private set; }
        public Guid? FuncionarioId { get; private set; }
        public Guid EmpresaId { get;private set; }
        public virtual Funcionario  Funcionario { get;private set; }
        public virtual Empresa Empresa { get;private set; }

        public override bool IsValid()
        {
            RuleFor(x => x.Nome)
               .NotEmpty().WithMessage("O Nome precisa ser fornecido")
               .Length(2, 100).WithMessage("O Nome precisa ter entre 2 e 100 caracteres");

            RuleFor(x => x.Descricao)
               .NotEmpty().WithMessage("O Nome precisa ser fornecido");

            RuleFor(c => c.DataHoraInicio).NotEmpty().WithMessage("A data hora inicio precisa ser fornecida")
                 .LessThan(c => c.DataHoraFim)
                 .WithMessage("A data de início deve ser menor que a data do final do evento");

            RuleFor(c => c.DataHoraInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage("A data de início não deve ser menor que a data atual");

            RuleFor(x => x.DataHoraFim)
                  .NotEmpty().WithMessage("A Data/Hora fim precisa ser fornecida");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid; 
        }

        public void AtualizarAtividade(string descricao, string nome, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            this.Descricao = descricao;
            this.Nome = nome;
            this.DataHoraInicio = dataHoraInicio;
            this.DataHoraFim = dataHoraFim;
        }
    }
}
