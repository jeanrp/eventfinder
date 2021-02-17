using EventFinder.Domain.Core.Commands;
using System;

namespace EventFinder.Domain.Clientes.Commands
{
    public class AdicionarFotoCommand : Command
    {

        public AdicionarFotoCommand(Guid id, string descricao, byte[] imagem, Guid? eventoId, Guid? clienteId, Guid? empresaId)
        {
            Id = id;
            Descricao = descricao;
            Imagem = imagem;
            EventoId = eventoId;
            ClienteId = clienteId;
            EmpresaId = empresaId;
        }

        public Guid Id { get;private set; }
        public string Descricao { get; private set; }
        public byte[] Imagem { get; private set; }
        public Guid? EventoId { get; private set; }
        public Guid? ClienteId { get; private set; }
        public Guid? EmpresaId { get; private set; }

        public void AtribuirImagem(byte[] imagem)
        {
            this.Imagem = imagem;
        }

       
    }
}
