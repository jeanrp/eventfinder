using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class IncluirEnderecoCommand : Command
    {
        public IncluirEnderecoCommand(Guid id, string logradouro, string numero, string complemento, string cep, string bairro, IncluirCidadeCommand cidade)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
        }

        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public IncluirCidadeCommand Cidade { get; private set; }
    }
}
