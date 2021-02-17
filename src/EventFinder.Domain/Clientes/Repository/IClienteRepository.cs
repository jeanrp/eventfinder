using EventFinder.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Clientes.Repository
{
    public interface IClienteRepository : IRepository<Cliente>  
    {
        Foto BuscarFotoPorId(Guid id);
        IngressoComprado BuscarIngressoCompradoPorId(Guid clienteId, Guid ingressoId);
        MensagemOrganizacaoEvento BuscarMensagemOrganizacaoEventoPorId(Guid clienteId, Guid empresaId);
        Publicacao BuscarPublicacaoPorId(Guid clienteId, Guid eventoId);
        Avaliacao BuscarAvaliacaoPorId(Guid clienteId, Guid eventoId);
        void IncluirAvaliacao(Avaliacao avaliacao);
        void AtualizarAvaliacao(Avaliacao avaliacao);
        void RemoverAvaliacao(Guid clienteId, Guid eventoId);
        IEnumerable<IngressoComprado> BuscarIngressosCompradosPorCliente(int clienteId);
        IEnumerable<MensagemOrganizacaoEvento> BuscarMensagensEnviadasERecebidasPorCliente(int clienteId);
        void EnviarMensagemOrganizacaoEvento(MensagemOrganizacaoEvento mensagem);
        void IncluirPublicacao(Publicacao publicacao);
        void AlterarPublicacao(Publicacao publicacao);
        void RemoverPublicacao(Guid clienteId, Guid eventoId);
        void IncluirFoto(Foto foto);
        void ExcluirFoto(Guid fotoId);
        IEnumerable<Foto> BuscarFotosPorCliente(Guid clienteId);        
        void ComprarIngresso(IngressoComprado ingressoComprado);
        void ParticiparPromocao(ClientePromocao clientePromocao);
        void RemoverParticipacaoPromocao(Guid clienteId, Guid promocaoId);
    }
}
