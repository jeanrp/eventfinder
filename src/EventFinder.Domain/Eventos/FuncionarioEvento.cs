using EventFinder.Domain.Core.Models;
using EventFinder.Domain.Empresas;
using System;

namespace EventFinder.Domain.Eventos
{
    public class FuncionarioEvento : Entity<FuncionarioEvento>
    {
        protected FuncionarioEvento() { }

        public FuncionarioEvento(Guid funcionarioId, Guid eventoId)
        {
            FuncionarioId = funcionarioId;
            EventoId = eventoId;
        }

        public Guid FuncionarioId { get; set; }
        public Guid EventoId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Evento Evento { get; set; }


        public void AtribuirEvento(Evento evento)
        {
            this.Evento = evento;
        }

        public void AtribuirFuncionario(Funcionario funcionario)
        {
            this.Funcionario = funcionario;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
