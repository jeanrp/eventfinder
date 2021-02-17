using System;
using EventFinder.Domain.Core.Models;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace EventFinder.Domain.Empresas
{
    public class Equipe : Entity<Equipe>
    {
        protected Equipe()
        { 
        }

        public Equipe(Guid id,string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }

        public string Nome { get;private set; }
        public string Descricao { get;private set; }

        public Guid EmpresaId { get;private set; }
        public virtual Empresa Empresa { get; private set; }
        public IEnumerable<Funcionario> Funcionarios { get => _funcionarios; }
        private readonly List<Funcionario> _funcionarios = new List<Funcionario>();

        public void AdicionarFuncionarios(Funcionario funcionario)
        {
            this._funcionarios.Add(funcionario);
        }

        public void AtualizarEquipe(string nome, string descricao)
        {
            this.Nome = nome;
            this.Descricao = descricao;
        }


        public override bool IsValid()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 100).WithMessage("O nome deve ter de 2 a 100 caracteres");

            RuleFor(x => x.Descricao)
                .Length(2, 500).WithMessage("A descrição deve ter de 2 a 500 caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;            
        }
    }
}
