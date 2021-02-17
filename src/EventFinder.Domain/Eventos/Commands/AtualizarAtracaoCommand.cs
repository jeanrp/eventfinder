using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class AtualizarAtracaoCommand : Command
    {
        public AtualizarAtracaoCommand(Guid id, string nome, string estilo)
        {
            Id = id;
            Nome = nome;
            Estilo = estilo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Estilo { get; private set; }
    }
}
