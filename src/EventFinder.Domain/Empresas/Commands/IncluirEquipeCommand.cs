using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class IncluirEquipeCommand : Command
    {
        public IncluirEquipeCommand(Guid id, string nome, string descricao, Guid empresaId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            EmpresaId = empresaId;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Guid EmpresaId { get; private set; }
    }
}
