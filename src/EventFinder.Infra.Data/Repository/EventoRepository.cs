using Dapper;
using EventFinder.Domain.Clientes;
using EventFinder.Domain.Empresas;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Eventos.Repository;
using EventFinder.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Infra.Data.Repository
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(EventosContext context) : base(context)
        {
        }

        public override IEnumerable<Evento> ObterTodos()
        {

            var sql = "SELECT" +
                         " ev.Id," +
                         "ev.Nome," +
                         "ev.*," +
                         "f.Id," +
                         "f.Imagem," +
                         "f.*," +
                         "e.Id," +
                         "e.Logradouro," +
                         "e.*," +
                         "c.Id," +
                         "c.Nome," +
                         "c.*," +
                         "ae.AtracaoId," +
                         "ae.EventoId," +
                         "a.Id," +
                         "a.Estilo," +
                         "a.*," +
                         "i.Id," +
                         "i.Preco," +
                         "i.*" +
                         " FROM Eventos ev" +
                         " LEFT JOIN Fotos f" +
                         " on ev.Id = f.EventoId" +
                         " INNER JOIN Enderecos e" +
                         " on ev.EnderecoId = e.Id" +
                         " INNER JOIN CIDADES c " +
                         " on e.CidadeId = c.Id" +
                         " INNER JOIN AtracoesEventos ae" +
                         " on ev.Id = ae.EventoId" +
                         " LEFT JOIN Atracoes a" +
                         " on ae.AtracaoId = a.Id" +
                         " LEFT JOIN Ingressos i" +
                         " on ev.Id = i.EventoId";


            Dictionary<Guid, Evento> lookupEvento = new Dictionary<Guid, Evento>();
            var eventos = this.Db.Database.GetDbConnection().
                Query<Evento, Foto, Endereco,Cidade, AtracaoEvento, Atracao, Ingresso, Evento>(sql,
                (ev, f, e, c, ae, a, i) =>
                {
                    Evento evento;


                    if (!lookupEvento.TryGetValue(ev.Id, out evento))
                    {
                        lookupEvento.Add(ev.Id, evento = ev);
                    }

                    if (f != null)
                        evento.AdicionarFotos(f);

                    if (ae != null)
                    {
                        if (a != null)
                            ae.AtribuirAtracao(a);

                        evento.AdicionarAtracoesEvento(ae);
                    }

                    if (i != null)
                        evento.AdicionarIngressos(i);


                    if (e != null)
                    {
                        if (c != null)                        
                            e.AtribuirCidade(c);
                        
                        evento.AtribuirEndereco(e);
                    }

                    return evento;
                },
                splitOn: "Imagem,Logradouro,Nome,AtracaoId,Estilo,Preco");


            return eventos;
        }

        public void AlterarAtracao(Atracao atracao)
        {
            this.Db.Set<Atracao>().Update(atracao);
        }

        public void AlterarEndereco(Endereco endereco)
        {
            this.Db.Set<Endereco>().Update(endereco);
        }

        public void AlterarIngresso(Ingresso ingresso)
        {
            this.Db.Set<Ingresso>().Update(ingresso);
        }

        public void AlterarPromocao(Promocao promocao)
        {
            this.Db.Set<Promocao>().Update(promocao);
        }

        public Atracao BuscarAtracaoPorId(Guid id)
        {
            var sql = "SELECT * FROM Atracoes where Atracoes.Id = @iId";

            var atracao = this.Db.Database.GetDbConnection().QueryFirstOrDefault<Atracao>(sql, new { iId = id });

            return atracao;
        }

        public Ingresso BuscarIngressoPorId(Guid id)
        {
            var sql = "SELECT * FROM Ingressos where Ingressos.Id = @iId";

            var ingresso = this.Db.Database.GetDbConnection().QueryFirstOrDefault<Ingresso>(sql, new { iId = id });

            return ingresso;
        }


        public Endereco BuscarEnderecoPorId(Guid id)
        {
            var sql = "SELECT * FROM Enderecos where Enderecos.Id = @iId";

            var endereco = this.Db.Database.GetDbConnection().QueryFirstOrDefault<Endereco>(sql, new { iId = id });

            return endereco;
        }


        public IEnumerable<AtracaoEvento> BuscarAtracoesPorEvento(Guid eventoId)
        {
            var sql = "SELECT e.Id, e.*, ae.AtracaoId, ae.*, a.Id,a.* FROM Eventos e" +
                    " INNER JOIN AtracoesEventos ae" +
                    " on e.Id = ae.EventoId" +
                    " INNER JOIN Atracoes a" +
                    " on ae.AtracaoId = a.Id" +
                    " where e.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, AtracaoEvento, Atracao, Evento>
                (sql, (e, ae, a) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(e.Id, out evento))
                    {
                        lookup.Add(e.Id, evento);
                        evento = e;
                    }


                    if (a != null)
                        ae.AtribuirAtracao(a);

                    if (ae != null)
                        evento.AdicionarAtracoesEvento(ae);


                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,Id");

            return even.FirstOrDefault().AtracoesEventos;
        }

        public Evento BuscarClientesQueCompraramIngressoNoEvento(Guid eventoId)
        {
            var sql = "SELECT ev.Id, ev.*, i.Id, i.*, ic.ClienteId, ic.*, c.Id FROM Eventos ev" +
                      " INNER JOIN Ingressos i" +
                      " on i.EventoId = ev.Id" +
                      " INNER JOIN IngressosComprados ic" +
                      " on i.Id = ic.ClienteId" +
                      " INNER JOIN Clientes c" +
                      " on ic.ClienteId = c.Id" +
                      " where ev.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Ingresso, IngressoComprado, Cliente, Evento>
                (sql, (ev, i, ic, c) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(ev.Id, out evento))
                    {
                        lookup.Add(ev.Id, evento);
                        evento = ev;
                    }

                    if (i != null)
                    {
                        if (ic != null)
                        {
                            if (c != null) ic.AtribuirClientes(c);

                            i.AdicionarIngressosComprados(ic);
                        }

                        ev.AdicionarIngressos(i);
                    }


                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,Id,ClienteId,Id");

            return even.FirstOrDefault();
        }



        public IEnumerable<Evento> BuscarEventoPorCategoria(Guid categoriaId)
        {
            var sql = "SELECT e.Id, e.*, c.Id, c.* FROM Eventos e" +
                      " inner join Categorias c" +
                      " on e.CategoriaId = c.Id" +
                      " where c.Id = @cCategoriaId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Categoria, Evento>
                (sql, (ev, c) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(ev.Id, out evento))
                    {
                        lookup.Add(ev.Id, evento);
                        evento = ev;
                    }

                    if (c != null) ev.AtribuirCategoriaEvento(c);

                    return evento;
                },
                new { cCategoriaId = categoriaId },
                splitOn: "Id,Id");

            return even;
        }

        public IEnumerable<Foto> BuscarFotosDoEvento(Guid eventoId)
        {
            var sql = "SELECT e.Id, e.*, f.Id, f.* FROM EVENTOs e" +
                      " inner join Fotos f" +
                      " on e.Id = f.EventoId" +
                      " where e.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Categoria, Evento>
                (sql, (ev, c) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(ev.Id, out evento))
                    {
                        lookup.Add(ev.Id, evento);
                        evento = ev;
                    }

                    if (c != null) ev.AtribuirCategoriaEvento(c);

                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,Id");

            return even.FirstOrDefault().Fotos;
        }

        public Evento BuscarFuncionariosEvento(Guid eventoId)
        {
            var sql = "SELECT e.Id, e.*, fe.EventoId, fe.*, f.Id, f.* FROM Eventos e" +
                      " inner join FuncionarioEventos fe" +
                      " ON e.Id = fe.EventoId" +
                      " inner join Funcionarios f" +
                      " ON fe.FuncionarioId = f.Id" +
                      " where e.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, FuncionarioEvento, Funcionario, Evento>
                (sql, (e, fe, f) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(e.Id, out evento))
                    {
                        lookup.Add(e.Id, evento);
                        evento = e;
                    }

                    if (fe != null)
                    {
                        if (f != null) fe.AtribuirFuncionario(f);

                        evento.AdicionarFuncionariosEventos(fe);
                    }


                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,EventoId,Id");

            return even.FirstOrDefault();
        }


        public Evento BuscarIngressosVendidosPorEvento(Guid eventoId)
        {
            var sql = "SELECT ev.Id , ev.* , i.Id, i.*, ic.ClienteId, ic.* FROM Eventos ev " +
                      " INNER JOIN Ingressos i" +
                      " on ev.Id = i.EventoId" +
                      " INNER JOIN IngressosComprados ic" +
                      " on i.Id = ic.IngressoId" +
                      " where ev.Id = @eEventoId ";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Ingresso, IngressoComprado, Evento>
                (sql, (e, i, ic) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(e.Id, out evento))
                    {
                        lookup.Add(e.Id, evento);
                        evento = e;
                    }

                    if (i != null)
                    {
                        if (ic != null) i.AdicionarIngressosComprados(ic);

                        e.AdicionarIngressos(i);
                    }

                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,Id,ClienteId");

            return even.FirstOrDefault();
        }

        public IEnumerable<Promocao> BuscarPromocoesAtivasDoEvento(Guid eventoId)
        {
            var sql = "SELECT e.Id, e.*,  p.ClienteId, p.* FROM Eventos e" +
                      " inner join Promocoes p" +
                      " on e.Id = p.EventoId" +
                      " where p.Situacao = 'A'" +
                      " and e.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Promocao, Evento>
                (sql, (e, p) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(e.Id, out evento))
                    {
                        lookup.Add(e.Id, evento);
                        evento = e;
                    }

                    if (p != null)
                        e.AdicionarPromocoes(p);

                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,ClienteId");

            return even.FirstOrDefault().Promocoes;
        }

        public Evento ObterEventoCompletoPaginaPorId(Guid id)
        {
            var sql = "SELECT E.Id, E.*, I.Id , I.*, en.Id, en.*, ae.AtracaoId, ae.*, a.Id, a.* FROM Eventos E " +
                    " inner join Ingressos I" +
                    " ON E.Id = I.EventoId" +
                    " inner join Enderecos en" +
                    " ON e.EnderecoId = E.EnderecoId" +
                    " inner join  AtracoesEventos ae" +
                    " on e.Id = ae.EventoId" +
                    " inner join Atracoes a" +
                    " on ae.AtracaoId = a.Id" +
                    " where e.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Ingresso, Endereco, AtracaoEvento, Atracao, Evento>
                (sql, (e, i, en, ae, a) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(e.Id, out evento))
                    {
                        lookup.Add(e.Id, evento);
                        evento = e;
                    }

                    if (i != null) evento.AdicionarIngressos(i);

                    if (en != null) evento.AtribuirEndereco(en);


                    if (ae != null)
                    {
                        if (a != null)
                            ae.AtribuirAtracao(a);

                        evento.AdicionarAtracoesEvento(ae);
                    }

                    return evento;
                },
                new { eEventoId = id },
                splitOn: "Id,Id,Id,AtracaoId,Id");

            return even.FirstOrDefault();

        }

        public IEnumerable<Publicacao> BuscarPublicacoesPorEvento(Guid eventoId)
        {
            var sql = "SELECT e.Id, e.*,  p.ClienteId, p.* FROM Eventos e" +
                                  " inner join Publicacoes p" +
                                  " on e.Id = p.EventoId" +
                                  " and e.Id = @eEventoId";

            Dictionary<Guid, Evento> lookup = new Dictionary<Guid, Evento>();
            var even = this.Db.Database.GetDbConnection().
                Query<Evento, Publicacao, Evento>
                (sql, (e, p) =>
                {
                    Evento evento;
                    if (!lookup.TryGetValue(e.Id, out evento))
                    {
                        lookup.Add(e.Id, evento);
                        evento = e;
                    }

                    if (p != null)
                        e.AdicionarPublicacoes(p);

                    return evento;
                },
                new { eEventoId = eventoId },
                splitOn: "Id,ClienteId");

            return even.FirstOrDefault().Publicacoes;
        }

        public IEnumerable<Promocao> BuscarTodasPromocoes()
        {
            return Db.Set<Promocao>().AsEnumerable();
        }

        public void ExcluirFoto(Guid fotoId)
        {
            Db.Set<Foto>().Remove(Db.Set<Foto>().AsNoTracking().FirstOrDefault(x => x.Id == fotoId));
        }

        public void IncluirAtracao(Atracao atracao)
        {
            Db.Set<Atracao>().Add(atracao);
        }

        public void IncluirEndereco(Endereco endereco)
        {
            Db.Set<Endereco>().Add(endereco);
        }

        public void IncluirFoto(Foto foto)
        {
            Db.Set<Foto>().Add(foto);
        }

        public void IncluirIngresso(Ingresso ingresso)
        {
            Db.Set<Ingresso>().Add(ingresso);
        }

        public void IncluirPromocao(Promocao promocao)
        {
            Db.Set<Promocao>().Add(promocao);
        }

        public void RemoverAtracao(Guid atracaoId)
        {
            Db.Set<Atracao>().Remove(Db.Set<Atracao>().AsNoTracking().FirstOrDefault(x => x.Id == atracaoId));
        }

        public void RemoverIngresso(Guid ingressoId)
        {
            Db.Set<Ingresso>().Remove(Db.Set<Ingresso>().AsNoTracking().FirstOrDefault(x => x.Id == ingressoId));
        }

        public void RemoverPromocao(Guid promocaoId)
        {
            Db.Set<Promocao>().Remove(Db.Set<Promocao>().AsNoTracking().FirstOrDefault(x => x.Id == promocaoId));
        }

        public void RemoverAtracaoEvento(Guid atracaoId, Guid eventoId)
        {
            Db.Set<AtracaoEvento>().Remove(Db.Set<AtracaoEvento>().AsNoTracking().FirstOrDefault(x => x.AtracaoId == atracaoId && x.EventoId == eventoId));
        }

        public void RemoverEndereco(Guid enderecoId)
        {
            Db.Set<Endereco>().Remove(Db.Set<Endereco>().AsNoTracking().FirstOrDefault(x => x.Id == enderecoId));
        }

        public void IncluirAtracaoEvento(AtracaoEvento atracaoEvento)
        {
            Db.Set<AtracaoEvento>().Add(atracaoEvento);
        }

        public IEnumerable<Estado> ObterEstados()
        {
            var sql = "SELECT * FROM ESTADOS ORDER BY UF ASC";

            return Db.Database.GetDbConnection().Query<Estado>(sql);
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            var sql = "SELECT * FROM CATEGORIAS ORDER BY DESCRICAO ASC";

            return Db.Database.GetDbConnection().Query<Categoria>(sql);
        }

        public void IncluirCidade(Cidade cidade)
        {
            this.Db.Set<Cidade>().Add(cidade);
        }

        public void AtualizarFoto(Foto foto)
        {
            this.Db.Set<Foto>().Update(foto);
        }

        public Foto BuscarFotoPorId(Guid fotoId)
        {
            return Db.Set<Foto>().AsNoTracking().FirstOrDefault(x => x.Id == fotoId);
        }


    }
}
