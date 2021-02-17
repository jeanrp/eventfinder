using System;

namespace EventFinder.Domain.Eventos.Commands
{
    public class AtualizarEventoCommand : BaseEventoCommand
    {

        public AtualizarEventoCommand(string nome, string descricao, string subDescricao, string descPatrocinadores, DateTime dataHoraInicio, DateTime dataHoraFim, string situacao, Guid categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            SubDescricao = subDescricao;
            DescPatrocinadores = descPatrocinadores;
             DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Situacao = situacao;
            CategoriaId = categoriaId;
        }
    }
}
