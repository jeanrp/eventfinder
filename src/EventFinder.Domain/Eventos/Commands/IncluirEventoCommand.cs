using EventFinder.Domain.Clientes.Commands;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Eventos.Commands
{
    public class IncluirEventoCommand : BaseEventoCommand
    {
        public IncluirEventoCommand(string nome,
            string descricao, 
            string subDescricao,
            string descPatrocinadores,             
            DateTime dataHoraInicio,
            DateTime dataHoraFim, 
            Guid categoriaId,
            Guid empresaId,
            IncluirEnderecoCommand endereco, IEnumerable<IncluirAtracaoCommand> atracoes, IEnumerable<IncluirIngressoCommand> ingressos, IEnumerable<AdicionarFotoCommand> fotos)
        {
            Nome = nome;
            Descricao = descricao;
            SubDescricao = subDescricao;
            DescPatrocinadores = descPatrocinadores;            
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim; 
            CategoriaId = categoriaId;
            EmpresaId = empresaId;
            Endereco = endereco;
            Atracoes = atracoes;
            Ingressos = ingressos;
            Fotos = fotos;
        }

        public IncluirEnderecoCommand Endereco { get; private set; }

        public IEnumerable<IncluirAtracaoCommand> Atracoes { get; private set; }

        public IEnumerable<IncluirIngressoCommand> Ingressos { get;private set; }

        public IEnumerable<AdicionarFotoCommand> Fotos { get; private set; }

    }
}
