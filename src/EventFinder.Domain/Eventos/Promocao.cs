using EventFinder.Domain.Clientes;
using EventFinder.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Domain.Eventos
{
    public class Promocao : Entity<Promocao>
    {

        protected Promocao()
        {
         }

        public Promocao(Guid id,string nome, string descricao, DateTime dataHoraInicio, DateTime dataHoraFim, string situacao, string nomeVencedor, Guid eventoId, Guid? clienteId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Situacao = situacao;
            NomeVencedor = nomeVencedor;
            EventoId = eventoId;
            ClienteId = clienteId;
        }

        public string Nome { get;private set; }

        public string Descricao { get; private set; }

        public DateTime DataHoraInicio { get; private set; }

        public DateTime DataHoraFim { get; private set; }

        public string Situacao { get; set; }

        public string NomeVencedor { get; private set; }

        public Guid EventoId { get; private set; }

        public Guid? ClienteId { get; private set; }

        public virtual Evento Evento { get; private set; }

        public IEnumerable<ClientePromocao> ClientesPromocoes => _clientesPromocoes; 
        private readonly List<ClientePromocao> _clientesPromocoes = new List<ClientePromocao>();    

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
