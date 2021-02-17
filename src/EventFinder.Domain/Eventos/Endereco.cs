using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Empresas;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFinder.Domain.Eventos
{
    public class Endereco : Entity<Endereco>
    {

        protected Endereco() { }

        public Endereco(Guid id, string logradouro, string numero, string complemento, string cep, string bairro, Cidade cidade)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
        }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public Guid CidadeId { get; private set; }
        public virtual Cidade Cidade { get; private set; }

        public virtual Evento Evento { get; private set; }
        public virtual Empresa Empresa { get; set; }


        public void AtribuirCidade(Cidade cidade)
        {
            this.Cidade = cidade;
        }

        [NotMapped]
        public string EnderecoFormatado
        {
            get
            {
                if (this.Cidade == null)
                    return string.Empty;

                return this.Logradouro + "," + this.Bairro + "," + this.Cidade.Nome;
            }
        }


        public void AtualizarEndereco(string logradouro, string numero, string complemento, string cep, string bairro, Guid cidadeId)
        {
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Cep = cep;
            this.Bairro = bairro;
            this.CidadeId = cidadeId;
        }

        public override bool IsValid()
        {
            RuleFor(c => c.Logradouro)
                            .NotEmpty().WithMessage("O Logradouro precisa ser fornecido")
                            .Length(2, 150).WithMessage("O Logradouro precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O Bairro precisa ser fornecido")
                .Length(2, 150).WithMessage("O Bairro precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage("O CEP precisa ser fornecido")
                .Length(8).WithMessage("O CEP precisa ter 8 caracteres");

            RuleFor(c => c.CidadeId).NotNull().WithMessage("A Cidade precisa ser fornecida");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O Numero precisa ser fornecido")
                .Length(1, 10).WithMessage("O Numero precisa ter entre 1 e 10 caracteres");

            ValidarCidade();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public void ValidarCidade()
        {
            if (Cidade.IsValid()) return;

            foreach (var error in Cidade.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

    }
}
