using EventFinder.Domain.Clientes;
using EventFinder.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Eventos.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        Evento ObterEventoCompletoPaginaPorId(Guid id);
        void IncluirCidade(Cidade Cidade);
        void IncluirAtracao(Atracao atracao);
        void IncluirAtracaoEvento(AtracaoEvento atracaoEvento);
        void AlterarAtracao(Atracao atracao);
        void RemoverAtracao(Guid atracaoId);
        void RemoverAtracaoEvento(Guid atracaoId,Guid eventoId);
        IEnumerable<AtracaoEvento> BuscarAtracoesPorEvento(Guid eventoId);
        void IncluirIngresso(Ingresso ingresso);
        void AlterarIngresso(Ingresso ingresso);
        void RemoverIngresso(Guid ingressoId);
        void IncluirPromocao(Promocao promocao);
        void AlterarPromocao(Promocao promocao);
        void RemoverPromocao(Guid promocaoId);
        void IncluirEndereco(Endereco endereco);
        void AlterarEndereco(Endereco endereco);
        void RemoverEndereco(Guid enderecoId);
        IEnumerable<Estado> ObterEstados();
        IEnumerable<Categoria> ObterCategorias();
        Endereco BuscarEnderecoPorId(Guid id);
        Atracao BuscarAtracaoPorId(Guid id);
        Ingresso BuscarIngressoPorId(Guid id);
        void IncluirFoto(Foto foto);
        void AtualizarFoto(Foto foto);
        void ExcluirFoto(Guid fotoId);
        Foto BuscarFotoPorId(Guid fotoId);
        Evento BuscarClientesQueCompraramIngressoNoEvento(Guid eventoId);
        IEnumerable<Evento> BuscarEventoPorCategoria(Guid categoriaId);
        IEnumerable<Promocao> BuscarTodasPromocoes();
        IEnumerable<Promocao> BuscarPromocoesAtivasDoEvento(Guid eventoId);
        IEnumerable<Publicacao> BuscarPublicacoesPorEvento(Guid eventoId); 
        Evento BuscarIngressosVendidosPorEvento(Guid eventoId);
        Evento BuscarFuncionariosEvento(Guid eventoId);
        IEnumerable<Foto> BuscarFotosDoEvento(Guid eventoId);       
    }
}
