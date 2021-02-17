using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Empresas.Commands
{
    public class IncluirAtividadeCommand : Command
    {
        public IncluirAtividadeCommand(Guid id, string descricao, string nome, DateTime dataHoraInicio, DateTime dataHoraFim, Guid empresaId)
        {
            Id = id;
            Descricao = descricao;
            Nome = nome;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            EmpresaId = empresaId;
        }

        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public Guid EmpresaId { get; private set; }
    }
}
