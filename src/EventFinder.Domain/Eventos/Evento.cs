using EventFinder.Domain.Clientes;
using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Empresas;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EventFinder.Domain.Eventos
{
    public class Evento : Entity<Evento>
    {
        protected Evento()
        {

        }

        public Evento(Guid id, string nome, string descricao, string subDescricao, string descPatrocinadores, DateTime dataHoraInicio, DateTime dataHoraFim, string situacao, Guid categoriaId, Guid empresaId, Endereco endereco)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            SubDescricao = subDescricao;
            DescPatrocinadores = descPatrocinadores;
            DataInclusao = DateTime.Now;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Situacao = situacao;
            CategoriaId = categoriaId;
            EmpresaId = empresaId;
            Endereco = endereco;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string SubDescricao { get; private set; }
        public string DescPatrocinadores { get; private set; }
        public DateTime DataInclusao { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public string Situacao { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public Guid EnderecoId { get; private set; }
        public virtual Categoria Categoria { get; private set; }
        public virtual Empresa Empresa { get; private set; }
        public virtual Endereco Endereco { get; private set; }
        public IEnumerable<Avaliacao> Avaliacoes => _avaliacoes;
        private readonly IList<Avaliacao> _avaliacoes = new List<Avaliacao>();
        public IEnumerable<Publicacao> Publicacoes => _publicacoes;
        private readonly IList<Publicacao> _publicacoes = new List<Publicacao>();
        public IEnumerable<Ingresso> Ingressos => _ingressos;
        private readonly IList<Ingresso> _ingressos = new List<Ingresso>();
        public IEnumerable<Promocao> Promocoes => _promocoes;
        private readonly IList<Promocao> _promocoes = new List<Promocao>();
        public IEnumerable<FuncionarioEvento> FuncionariosEventos => _funcionariosEventos;
        private readonly IList<FuncionarioEvento> _funcionariosEventos = new List<FuncionarioEvento>();
        public IEnumerable<AtracaoEvento> AtracoesEventos => _atracoesEventos;
        private readonly IList<AtracaoEvento> _atracoesEventos = new List<AtracaoEvento>();
        public IEnumerable<Foto> Fotos => _fotos;
        private readonly List<Foto> _fotos = new List<Foto>();
 
        [NotMapped]
        public IList<Atracao> Atracoes = new List<Atracao>();

        public void AdicionarAtracoesEvento(AtracaoEvento atracaoEvento)
        {
            _atracoesEventos.Add(atracaoEvento);
        }

        public void AdicionarAvaliacoesEvento(Avaliacao avaliacao)
        {
            _avaliacoes.Add(avaliacao);
        }

        public void AtribuirCategoriaEvento(Categoria categoria)
        {
            this.Categoria = categoria;
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            this.Endereco = endereco;
        }

        public void AdicionarFuncionariosEventos(FuncionarioEvento fe)
        {
            this._funcionariosEventos.Add(fe);
        }

        public void AdicionarPromocoes(Promocao p)
        {
            this._promocoes.Add(p);
        }

        public void AdicionarPublicacoes(Publicacao p)
        {
            this._publicacoes.Add(p);
        }

        public void AdicionarIngressos(Ingresso ingresso)
        {
            _ingressos.Add(ingresso);
        }

        public void DistinctFotos(IEnumerable<Foto> fotos)
        {
            this._fotos.Clear();

            foreach (var item in fotos)
                _fotos.Add(item);


        }


        public void DistinctIngresso(IEnumerable<Ingresso> ingressos)
        {
            this._ingressos.Clear();

            foreach (var item in ingressos)
                _ingressos.Add(item);
        }

        public void AdicionarFotos(Foto foto)
        {
            _fotos.Add(foto);
        }

        public override bool IsValid()
        {
            RuleFor(x => x.Nome)
                      .NotEmpty().WithMessage("O Nome precisa ser fornecido")
                      .Length(2, 100).WithMessage("O Nome precisa ter entre 2 e 100 caracteres");

            RuleFor(x => x.Descricao)
                      .NotEmpty().WithMessage("A descrição precisa ser fornecida");

            RuleFor(x => x.SubDescricao)
                 .Length(2, 100).WithMessage("A Subdescrição precisa ter entre 2 e 100 caracteres");

            RuleFor(x => x.DescPatrocinadores)
                .Length(2, 100).WithMessage("Os patrocinadores devem ter entre 2 e 100 caracteres");

            RuleFor(c => c.DataHoraInicio).NotEmpty().WithMessage("A data hora inicio precisa ser fornecida")
               .LessThan(c => c.DataHoraFim)
               .WithMessage("A data de início deve ser menor que a data do final do evento");

            RuleFor(c => c.DataHoraInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage("A data de início não deve ser menor que a data atual");

            RuleFor(x => x.DataHoraFim).NotEmpty().WithMessage("A data hora fim precisa ser fornecida");


            ValidarEndereco();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        public void AtualizarEvento(string nome,
            string descricao,
            string subDescricao,
            string descPatrocinadores,
            DateTime dataHoraInicio,
            DateTime dataHoraFim,
            string situacao,
            Guid categoriaId)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.SubDescricao = subDescricao;
            this.DescPatrocinadores = descPatrocinadores;
            this.DataHoraInicio = dataHoraInicio;
            this.DataHoraFim = dataHoraFim;
            this.Situacao = situacao;
            this.CategoriaId = categoriaId;
        }

        private void ValidarEndereco()
        {
            if (Endereco.IsValid()) return;

            foreach (var error in Endereco.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }


    }
}
