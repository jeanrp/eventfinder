using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Eventos.Events
{
    public class EnderecoEventoAdicionadoEvent : Event
    {
        public EnderecoEventoAdicionadoEvent(Guid id, string logradouro, string numero, string complemento, string cep, string bairro)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Bairro = bairro;
            AggregateId = id;
        }

        public Guid Id { get;private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
    }
}
