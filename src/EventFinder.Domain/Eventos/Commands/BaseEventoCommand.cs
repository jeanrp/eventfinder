using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public abstract class BaseEventoCommand : Command
    {

        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string SubDescricao { get; protected set; }
        public string DescPatrocinadores { get; protected set; }
        
        public string Situacao { get;protected set; }
        public DateTime DataHoraInicio { get; protected set; }
        public DateTime DataHoraFim { get; protected set; } 
        public Guid CategoriaId { get; protected set; }
        public Guid EmpresaId { get; protected set; }
        public Guid EnderecoId { get; protected set; }

    }
}
