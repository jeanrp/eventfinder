using Dapper;
using EventFinder.Domain.Clientes;
using EventFinder.Domain.Clientes.Repository;
using EventFinder.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(EventosContext context) : base(context)
        {
        }

        public void AlterarPublicacao(Publicacao publicacao)
        {
            this.Db.Set<Publicacao>().Update(publicacao);
        }

        public void AtualizarAvaliacao(Avaliacao avaliacao)
        {
            this.Db.Set<Avaliacao>().Update(avaliacao);
        }

        public Avaliacao BuscarAvaliacaoPorId(Guid clienteId, Guid eventoId)
        {
            return this.Db.Set<Avaliacao>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.EventoId == eventoId);
        }

        public Foto BuscarFotoPorId(Guid id)
        {
            return this.Db.Set<Foto>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Foto> BuscarFotosPorCliente(Guid clienteId)
        {
            var sql = "SELECT C.Id, C.*, F.Id, F.* FROM Clientes C" +
                      " INNER JOIN Fotos F" +
                      " ON C.Id = F.ClienteId" +
                      " where c.Id = @cClienteId";

            Dictionary<Guid, Cliente> lookup = new Dictionary<Guid, Cliente>();

            var cli = this.Db.Database.GetDbConnection().Query<Cliente, Foto, Cliente>(sql, (c, f) =>
           {
               Cliente cliente;
               if (!lookup.TryGetValue(c.Id, out cliente))
               {
                   lookup.Add(c.Id, cliente);
                   cliente = c;
               }
               if (f != null)
                   cliente.AdicionarFotos(f);


               return cliente;
           },
            new { cClienteId = clienteId },
           splitOn: "Id,Id");

            return cli.FirstOrDefault().Fotos;
        }

        public IngressoComprado BuscarIngressoCompradoPorId(Guid clienteId, Guid ingressoId)
        {
            return this.Db.Set<IngressoComprado>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.IngressoId == ingressoId);
        }

        public IEnumerable<IngressoComprado> BuscarIngressosCompradosPorCliente(int clienteId)
        {
            var sql = "SELECT c.Id, c.*, ic.Id, ic.* FROM Clientes  c" +
                      " inner join IngressosComprados ic" +
                      " on c.Id = ic.ClienteId" +
                      " where c.Id = @cClienteId";

            Dictionary<Guid, Cliente> lookup = new Dictionary<Guid, Cliente>();

            var cli = this.Db.Database.GetDbConnection().
                Query<Cliente, IngressoComprado, Cliente>
                (sql, (c, ic) =>
                {
                    Cliente cliente;
                    if (!lookup.TryGetValue(c.Id, out cliente))
                    {
                        lookup.Add(c.Id, cliente);
                        cliente = c;
                    }
                    if (ic != null)
                        cliente.AdicionarIngressos(ic);

                    return cliente;
                },
                new { cClienteId = clienteId },
                splitOn: "Id,Id");

            return cli.FirstOrDefault().IngressosComprados;
        }

        public MensagemOrganizacaoEvento BuscarMensagemOrganizacaoEventoPorId(Guid clienteId, Guid empresaId)
        {
            return this.Db.Set<MensagemOrganizacaoEvento>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.EmpresaId == empresaId);
        }

        public IEnumerable<MensagemOrganizacaoEvento> BuscarMensagensEnviadasERecebidasPorCliente(int clienteId)
        {
            var sql = "SELECT c.Id, c.*, moe.Id, moe.* FROM Clientes c" +
                      " INNER JOIN MensagensOrganizacoesEventos moe" +
                      " on c.Id = moe.ClienteId" +
                      " where c.Id = @cClienteId";

            Dictionary<Guid, Cliente> lookup = new Dictionary<Guid, Cliente>();

            var cli = this.Db.Database.GetDbConnection().
                Query<Cliente, MensagemOrganizacaoEvento, Cliente>
                (sql, (c, moe) =>
                {
                    Cliente cliente;
                    if (!lookup.TryGetValue(c.Id, out cliente))
                    {
                        lookup.Add(c.Id, cliente);
                        cliente = c;
                    }
                    if (moe != null)
                        cliente.AdicionarMensagens(moe);

                    return cliente;
                },
                new { cClienteId = clienteId },
                splitOn: "Id,Id");

            return cli.FirstOrDefault().Mensagens;
        }

        public Publicacao BuscarPublicacaoPorId(Guid clienteId, Guid eventoId)
        {
            return this.Db.Set<Publicacao>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.EventoId == eventoId);
        }

        public void ComprarIngresso(IngressoComprado ingressoComprado)
        {
            this.Db.Set<IngressoComprado>().Add(ingressoComprado);
        }

        public void EnviarMensagemOrganizacaoEvento(MensagemOrganizacaoEvento mensagem)
        {
            this.Db.Set<MensagemOrganizacaoEvento>().Add(mensagem);
        }

        public void ExcluirFoto(Guid fotoId)
        {
            this.Db.Set<Foto>().Remove(this.Db.Set<Foto>().AsNoTracking().FirstOrDefault(x => x.Id == fotoId));
        }

        public void IncluirAvaliacao(Avaliacao avaliacao)
        {
            this.Db.Set<Avaliacao>().Add(avaliacao);
        }

        public void IncluirFoto(Foto foto)
        {
            this.Db.Set<Foto>().Add(foto);
        }

        public void IncluirPublicacao(Publicacao publicacao)
        {
            this.Db.Set<Publicacao>().Add(publicacao);
        }

        public void ParticiparPromocao(ClientePromocao clientePromocao)
        {
            this.Db.Set<ClientePromocao>().Add(clientePromocao);
        }

        public void RemoverAvaliacao(Guid clienteId, Guid eventoId)
        {
            this.Db.Remove(this.Db.Set<Avaliacao>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.EventoId == eventoId));
        }

        public void RemoverParticipacaoPromocao(Guid clienteId, Guid promocaoId)
        {
            this.Db.Remove(this.Db.Set<ClientePromocao>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.PromocaoId == promocaoId));
        }

        public void RemoverPublicacao(Guid clienteId, Guid eventoId)
        {
            this.Db.Remove(this.Db.Set<Publicacao>().AsNoTracking().FirstOrDefault(x => x.ClienteId == clienteId && x.EventoId == eventoId));
        }
    }
}
