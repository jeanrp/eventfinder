using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class EditarEquipeCommand : Command
    {
        public EditarEquipeCommand(Guid id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
    }
}
