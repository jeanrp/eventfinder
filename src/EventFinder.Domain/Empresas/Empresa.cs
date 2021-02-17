using EventFinder.Domain.Clientes;
using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Funcionarios;
using EventFinder.Domain.Usuarios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Empresas
{
    public class Empresa : Entity<Empresa>
    {
        protected Empresa() { }

        public Empresa(Guid id, string razaoSocial, string email, string telefone, string facebook, string cnpj, Guid? enderecoId)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
        }

        public string RazaoSocial { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Facebook { get; private set; }
        public string Cnpj { get; private set; }

        public Guid? EnderecoId { get; private set; }
        public virtual Endereco Endereco { get; private set; }

         public virtual Usuario Usuario { get; private set; }

        public IEnumerable<Evento> Eventos { get => _eventos; }
        private readonly List<Evento> _eventos = new List<Evento>();

        public IEnumerable<Foto> Fotos { get => _fotos; }
        private readonly List<Foto> _fotos = new List<Foto>();

        public IEnumerable<MensagemOrganizacaoEvento> Mensagens => _mensagens; 
        private readonly List<MensagemOrganizacaoEvento> _mensagens = new List<MensagemOrganizacaoEvento>();

        public IEnumerable<Funcionario> Funcionarios => _funcionarios; 
        private readonly List<Funcionario> _funcionarios = new List<Funcionario>();

        public IEnumerable<Atividade> Atividades => _atividades;
        private readonly List<Atividade> _atividades = new List<Atividade>();

        public IEnumerable<Equipe> Equipes => _equipes; 
        private readonly List<Equipe> _equipes = new List<Equipe>();

        public void AtualizarEmpresa(string razaoSocial, string email, string telefone, string facebook, string cnpj)
        { 
            this.RazaoSocial = razaoSocial;
            this.Email = email;
            this.Telefone = telefone;
            this.Facebook = facebook;
            this.Cnpj = cnpj; 
        }


        public void AdicionarAtividade(Atividade atividade)
        {
            this._atividades.Add(atividade);
        }

        public void AdicionarEquipes(Equipe equipe)
        {
            this._equipes.Add(equipe);
        }

        public void AdicionarFotos(Foto foto)
        {
            this._fotos.Add(foto);
        }

        public void AdicionarFuncionarios(Funcionario funcionario)
        {
            this._funcionarios.Add(funcionario);
        }


        public void AdicionarMensagens(MensagemOrganizacaoEvento mensagem)
        {
            this._mensagens.Add(mensagem);
        }


        public void AdicionarEventos(Evento evento)
        {
            this._eventos.Add(evento);
        }



        public override bool IsValid()
        {
            RuleFor(x => x.RazaoSocial).NotEmpty().WithMessage("A Razão Social da Empresa é obrigatória!")
                .Length(2,150).WithMessage("A razão social deve ter de 2 a 150 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O email é obrigato'rio!")
                .Length(2, 150).WithMessage("A email deve ter de 2 a 150 caracteres")
                .EmailAddress().WithMessage("O email deve ser inserido num formato incorreto.");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("O telefone da empresa é obrigatório.")
                .Length(10,11).WithMessage("O telefone deve possuir a quantidade de caracteres necessários antes de salvar.");

            RuleFor(x => x.Facebook).
                Length(2,100).WithMessage("O facebook deve ter entre 2 e 100 caracteres");

          RuleFor(x => x.Cnpj).NotEmpty().WithMessage("O cnpj é obrigatório!")
                .Length(14).WithMessage("O formato do CNPJ está incorreto");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
