using EventFinder.Domain.Clientes;
using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Empresas;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Usuarios
{
    public class Usuario : Entity<Usuario>
    {
        protected Usuario() { }

        public Usuario(Guid id, string nomeRazaoSocial, DateTime dataHoraCadastro, string email, string senha, Empresa empresa= null, Guid? empresaId = null, Cliente cliente = null, Guid? clienteId = null)
        {
            Id = id;
            NomeRazaoSocial = nomeRazaoSocial;
            DataHoraCadastro = dataHoraCadastro;
            Email = email;
            Senha = senha;
            Empresa = empresa;
            EmpresaId = empresaId;
            Cliente = cliente;
            ClienteId = clienteId;
        }

        public string NomeRazaoSocial { get; private set; }

        public DateTime DataHoraCadastro { get; private set; }


        public string Email { get; private set; }

        public string Senha { get; private set; }

        public Guid? ClienteId { get; private set; }

        public Guid? EmpresaId { get; private set; }

        public virtual Cliente Cliente { get; private set; }


        public virtual Empresa Empresa { get; private set; }

        public IEnumerable<PermissaoUsuarioFuncao> Permissoes => _permissoes;
        private readonly IList<PermissaoUsuarioFuncao> _permissoes;

        public void AtualizarUsuario(string nomeRazaoSocial, string email, string senha)
        {
            this.NomeRazaoSocial = nomeRazaoSocial;
            this.Email = email;
            this.Senha = senha;
        }

        public override bool IsValid()
        {
            ValidarUsuario();
            ValidarEmpresa();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public bool IsValidCliente()
        {
            ValidarUsuario();
            ValidarCliente();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public void ValidarUsuario()
        {
            RuleFor(x => x.NomeRazaoSocial).NotEmpty().WithMessage("O Nome/Razão Social é obrigatório!")
              .Length(2, 150).WithMessage("O Nome/Razão Social deve ter de 2 a 150 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O email é obrigatório!")
                .Length(2, 150).WithMessage("A email deve ter de 2 a 150 caracteres")
                .EmailAddress().WithMessage("O email deve ser inserido num formato incorreto.");

            RuleFor(x => x.Senha).NotEmpty().WithMessage("A Senha  é obrigatória!")
             .Length(2, 150).WithMessage("A senha deve ter de 2 a 150 caracteres");

        }

        private void ValidarEmpresa()
        {
            if (Empresa.IsValid()) return;

            foreach (var error in Empresa.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

        private void ValidarCliente()
        {
            if (Cliente.IsValid()) return;

            foreach (var error in Cliente.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }
    }
}
