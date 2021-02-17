using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Funcionarios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Empresas
{
    public class Funcionario : Entity<Funcionario>
    {
        protected Funcionario() {  }

        public Funcionario(Guid id,string nome, string email, string telefone, string facebook, string cargo, string sexo, Guid empresaId, Guid equipeId)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cargo = cargo;
            Sexo = sexo;
            EmpresaId = empresaId;
            EquipeId = equipeId;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Facebook { get; private set; }
        public string Cargo { get; private set; }
        public string Sexo { get;private set; }        

        public Guid EmpresaId { get; private set; }
        public Guid EquipeId { get; private set; }

        public virtual Empresa Empresa { get; private set; }
        public virtual Equipe Equipe { get; private set; }

        public IEnumerable<Atividade> Atividades  => _atividades; 
        private readonly List<Atividade> _atividades = new List<Atividade>();

        public IEnumerable<FuncionarioEvento> FuncionariosEventos  => _funcionariosEventos; 
        private readonly List<FuncionarioEvento> _funcionariosEventos = new List<FuncionarioEvento>();


        public void AdicionarAtividade(Atividade atividade)
        {
            this._atividades.Add(atividade);
        }

        public void AtualizarFuncionario(string nome, string email, string telefone, string facebook, string cargo, string sexo, Guid equipeId)
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.Facebook = facebook;
            this.Cargo = cargo;
            this.Sexo = sexo;
            this.EquipeId = equipeId;
        }

        public override bool IsValid()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 150).WithMessage("O Nome deve ter entre 2 e 150 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O email é obrigatório")
                .Length(2, 150). WithMessage("O email deve ter entre 2 e 150 caracteres")
                .EmailAddress().WithMessage("O email informado deve ser um email válido");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("O telefone é obrigatório")
                .Length(10, 11).WithMessage("Digite a quantidade de digitos corretamente de um telefone");

            RuleFor(x => x.Facebook).Length(2, 100).WithMessage("O facebook deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.Cargo).NotEmpty().WithMessage("O cargo do funcionario é obrigatório")
                .Length(1, 50).WithMessage("O cargo deve ter entre 1 e 50 caracteres");

            RuleFor(x => x.Sexo).NotEmpty().WithMessage("O sexo do funcionario é obrigatório")
                .Length(1).WithMessage("O Sexo deve ter um caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
