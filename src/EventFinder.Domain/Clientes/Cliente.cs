using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Usuarios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Clientes
{
    public class Cliente : Entity<Cliente>
    {
        protected Cliente()
        {
            this._ingressosComprados = new List<IngressoComprado>();
            this._mensagens = new List<MensagemOrganizacaoEvento>();
            this._publicacoes = new List<Publicacao>();
            this._avaliacoes = new List<Avaliacao>(); 
            this._fotos = new List<Foto>();
            this._clientesPromocoes = new List<ClientePromocao>();
        }

        public Cliente(Guid id,string nome, string email, string telefone, string facebook, DateTime dataNascimento, string sexo, string estadoCivil, string atracaoPreferida, string estiloPreferido)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Facebook = facebook;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            AtracaoPreferida = atracaoPreferida;
            EstiloPreferido = estiloPreferido;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Facebook { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Sexo { get; private set; }
        public string EstadoCivil { get; private set; }
        public string AtracaoPreferida { get; private set; }
        public string EstiloPreferido { get; private set; } 
        public ICollection<MensagemOrganizacaoEvento> Mensagens { get => _mensagens; }
        private readonly IList<MensagemOrganizacaoEvento> _mensagens;
        public ICollection<IngressoComprado> IngressosComprados { get => _ingressosComprados; }
        private readonly IList<IngressoComprado> _ingressosComprados;
        public ICollection<Publicacao> Publicacoes { get => _publicacoes; }
        private readonly IList<Publicacao> _publicacoes;
        public ICollection<Avaliacao> Avaliacoes { get => _avaliacoes; }
        private readonly IList<Avaliacao> _avaliacoes;
        public ICollection<ClientePromocao> ClientesPromocoes { get => _clientesPromocoes; }
        private readonly IList<ClientePromocao> _clientesPromocoes;

        public ICollection<Foto> Fotos { get => _fotos; }
        private readonly IList<Foto> _fotos;
        public virtual Usuario Usuario { get; set; }

        public void AdicionarIngressos(IngressoComprado ingressoComprado)
        {
            this._ingressosComprados.Add(ingressoComprado);
        }

        public void AdicionarMensagens(MensagemOrganizacaoEvento mensagem)
        {
            this._mensagens.Add(mensagem);
        }

        public void AdicionarPublicacoes(Publicacao publicacao)
        {
            this._publicacoes.Add(publicacao);
        }

        public void AdicionarAvaliacoes(Avaliacao avaliacao)
        {
            this._avaliacoes.Add(avaliacao);
        }

        public void AdicionarFotos(Foto foto)
        {
            this._fotos.Add(foto);
        }

        public void AtualizarCliente(string nome, string email, string telefone, string facebook, DateTime dataNascimento, string sexo, string estadoCivil, string atracaoPreferida, string estiloPreferido)
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.Facebook = facebook;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.EstadoCivil = estadoCivil;
            this.AtracaoPreferida = atracaoPreferida;
            this.EstiloPreferido = estiloPreferido;
        }

        public override bool IsValid()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 150).WithMessage("O Nome deve ter entre 2 e 150 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O email é obrigatório")
                .Length(2, 150).WithMessage("O email deve ter entre 2 e 150 caracteres")
               .EmailAddress().WithMessage("O formato do email informado está incorreto");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("O telefone é obrigatório")
                .Length(2, 11).WithMessage("Preencha o telefone corretamente");

            RuleFor(x => x.Facebook).Length(2, 100).WithMessage("O facebook deve ter no máximo 100 caracteres");

            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("A data de nascimento é obrigatória");

            RuleFor(x => x.Sexo).NotEmpty().WithMessage("O sexo é obrigatório")
                .Length(1).WithMessage("O sexo deve ter 1 caractere");

            RuleFor(x => x.EstadoCivil).NotEmpty().WithMessage("O estado civil é obrigatório")
                    .Length(1).WithMessage("O estado civil deve ter 1 caractere");

            RuleFor(x => x.EstiloPreferido).Length(2,100).WithMessage("O estilo deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.AtracaoPreferida).Length(2, 100).WithMessage("A atracao preferida deve ter entre 2 e 100 caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
