using Dapper;
using EventFinder.Domain.Clientes;
using EventFinder.Domain.Empresas;
using EventFinder.Domain.Empresas.Repository;
using EventFinder.Domain.Funcionarios;
using EventFinder.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EventFinder.Domain.Eventos;

namespace EventFinder.Infra.Data.Repository
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(EventosContext context) : base(context)
        {
        }

        public void AlterarAtividade(Atividade atividade)
        {
            this.Db.Set<Atividade>().Update(atividade);
        }

        public void AlterarEquipe(Equipe equipe)
        {
            this.Db.Set<Equipe>().Update(equipe);
        }

        public void AlterarFuncionario(Funcionario funcionario)
        {
            this.Db.Set<Funcionario>().Update(funcionario);
        }

        public IEnumerable<Atividade> BuscarAtividadesPorEmpresa(Guid empresaId)
        {
            var sql = "SELECT e.Id, e.*, a.Id, a.* FROM Empresas e" +
                      " INNER JOIN Atividades a" +
                      " on e.Id = a.EmpresaId" +
                      " where e.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Atividade, Empresa>(sql,
                (e, a) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(e.Id, out empresa))
                    {
                        lookup.Add(e.Id, empresa);
                        empresa = e;
                    }
                    if (a != null)
                        empresa.AdicionarAtividade(a);

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id");

            return emp.FirstOrDefault().Atividades;
        }

        public IEnumerable<Atividade> BuscarAtividadesPorFuncionario(Guid funcionarioId)
        {
            var sql = "SELECT f.Id, f.*, a.Id, a.* FROM Funcionarios f" +
                               " INNER JOIN Atividades a" +
                               " on f.Id = a.FuncionarioId" +
                               " where f.Id = @fFuncionarioId";

            Dictionary<Guid, Funcionario> lookup = new Dictionary<Guid, Funcionario>();

            var func = this.Db.Database.GetDbConnection().
                Query<Funcionario, Atividade, Funcionario>(sql,
                (f, a) =>
                {
                    Funcionario funcionario;

                    if (!lookup.TryGetValue(f.Id, out funcionario))
                    {
                        lookup.Add(f.Id, funcionario);
                        funcionario = f;
                    }
                    if (a != null)
                        funcionario.AdicionarAtividade(a);

                    return funcionario;
                },
                new { fFuncionarioId = funcionarioId },
                splitOn: "Id,Id");

            return func.FirstOrDefault().Atividades;
        }

        public IEnumerable<Avaliacao> BuscarRelatorioAvaliacoesDosEventosDaEmpresa(Guid empresaId)
        {
            return null;
        }

        public IEnumerable<Equipe> BuscarEquipesPorEmpresa(Guid empresaId)
        {
            var sql = "SELECT em.Id, em.*, eq.Id, eq.* FROM Empresas em" +
                                          " INNER JOIN Equipes eq" +
                                          " on em.Id = eq.EmpresaId" +
                                          " where em.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Equipe, Empresa>(sql,
                (em, eq) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(em.Id, out empresa))
                    {
                        lookup.Add(em.Id, empresa);
                        empresa = em;
                    }
                    if (eq != null)
                        empresa.AdicionarEquipes(eq);

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id");

            return emp.FirstOrDefault().Equipes;
        }

        public IEnumerable<Evento> BuscarEventosPorEmpresa(Guid empresaId)
        {
            var sql = "SELECT" +
                      " em.RazaoSocial," +
                      "em.*," +
                      "ev.Nome," +
                      "ev.*," +
                      "f.Imagem," +
                      "f.*," +
                      "e.Logradouro," +
                      "e.*," +
                      "ae.AtracaoId," +
                      "ae.EventoId," +
                      "a.Estilo," +
                      "a.Id," +
                      "a.*," +
                      "i.Preco," +
                      "i.*" +
                      " FROM Empresas em" +
                      " INNER JOIN Eventos ev" +
                      " on em.Id = ev.EmpresaId" +
                      " LEFT JOIN Fotos f" +
                      " on ev.Id = f.EventoId" +
                      " INNER JOIN Enderecos e" +
                      " on ev.EnderecoId = e.Id" +
                      " INNER JOIN AtracoesEventos ae" +
                      " on ev.Id = ae.EventoId" +
                      " LEFT JOIN Atracoes a" +
                      " on ae.AtracaoId = a.Id" +
                      " LEFT JOIN Ingressos i" +
                      " on ev.Id = i.Id" +
                      " where em.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();
            Dictionary<Guid, Evento> lookupEvento = new Dictionary<Guid, Evento>();
            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Evento, Foto, Endereco, AtracaoEvento, Atracao, Ingresso, Empresa>(sql,
                (em, ev, f, e, ae, a, i) =>
                {
                    Empresa empresa;


                    if (!lookup.TryGetValue(em.Id, out empresa))
                    {
                        lookup.Add(em.Id, empresa = em);
                    }

                    if (ev != null)
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
                            evento.AtribuirEndereco(e);

                        empresa.AdicionarEventos(evento);
                    }

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Nome,RazaoSocial,Imagem,Logradouro,AtracaoId,Estilo,Preco");

            var empres = emp.FirstOrDefault();

            if (empres == null)
                return new List<Evento>();

            var lista = empres.Eventos;

            return lista == null || lista.Count() == 0 ? new List<Evento>() : lista;
        }

        public IEnumerable<Foto> BuscarFotosPorEmpresa(Guid empresaId)
        {
            var sql = "SELECT e.Id, e.*, f.Id, f.* FROM Empresas e" +
                                                     " INNER JOIN Fotos f" +
                                                     " on e.Id = f.EmpresaId" +
                                                     " where e.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Foto, Empresa>(sql,
                (e, f) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(e.Id, out empresa))
                    {
                        lookup.Add(e.Id, empresa);
                        empresa = e;
                    }

                    if (f != null)
                        empresa.AdicionarFotos(f);

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id");

            return emp.FirstOrDefault().Fotos;
        }

        public IEnumerable<Funcionario> BuscarFuncionariosPorEmpresa(Guid empresaId)
        {
            var sql = "SELECT e.Id, e.*, f.Id, f.* FROM Empresas e" +
                      " INNER JOIN Funcionarios f" +
                      " on e.Id = f.EmpresaId" +
                      " where e.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Funcionario, Empresa>(sql,
                (e, f) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(e.Id, out empresa))
                    {
                        lookup.Add(e.Id, empresa);
                        empresa = e;
                    }
                    if (f != null)
                        empresa.AdicionarFuncionarios(f);

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id");

            return emp.FirstOrDefault().Funcionarios;
        }

        public IEnumerable<Funcionario> BuscarFuncionariosPorEquipe(Guid equipeId)
        {
            var sql = "SELECT e.Id, e.*, f.Id, f.* FROM Equipes e" +
                      " INNER JOIN Funcionarios f" +
                      " on e.Id = f.EquipeId" +
                      " where e.Id = @eEquipeId";

            Dictionary<Guid, Equipe> lookup = new Dictionary<Guid, Equipe>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Equipe, Funcionario, Equipe>(sql,
                (e, f) =>
                {
                    Equipe equipe;

                    if (!lookup.TryGetValue(e.Id, out equipe))
                    {
                        lookup.Add(e.Id, equipe);
                        equipe = e;
                    }
                    if (f != null)
                        equipe.AdicionarFuncionarios(f);

                    return equipe;
                },
                new { eEquipeId = equipeId },
                splitOn: "Id,Id");

            return emp.FirstOrDefault().Funcionarios;
        }

        public IEnumerable<MensagemOrganizacaoEvento> BuscarMensagensPorEmpresa(Guid empresaId)
        {
            var sql = "SELECT e.Id, e.*, moe.EmpresaId, moe.* FROM Empresas e" +
                     " INNER JOIN MensagensOrganizacoesEventos moe" +
                     " on e.Id = moe.EmpresaId" +
                     " where e.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, MensagemOrganizacaoEvento, Empresa>(sql,
                (e, m) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(e.Id, out empresa))
                    {
                        lookup.Add(e.Id, empresa);
                        empresa = e;
                    }
                    if (m != null)
                        empresa.AdicionarMensagens(m);

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id");

            return emp.FirstOrDefault().Mensagens;
        }


        public bool EnviarEmailParaFuncionariosComAtividades(IEnumerable<Funcionario> funcionario)
        {
            throw new NotImplementedException();
        }

        public bool EnviarSmsParaFuncionariosComAtividades(IEnumerable<Funcionario> funcionario)
        {
            throw new NotImplementedException();
        }

        public void ExcluirFoto(Guid fotoId)
        {
            this.Db.Set<Foto>().Remove(this.Db.Set<Foto>().AsNoTracking().FirstOrDefault(x => x.Id == fotoId));
        }

        public void IncluirAtividade(Atividade atividade)
        {
            this.Db.Set<Atividade>().Add(atividade);
        }

        public void IncluirEquipe(Equipe equipe)
        {
            this.Db.Set<Equipe>().Add(equipe);
        }

        public void IncluirFoto(Foto foto)
        {
            this.Db.Set<Foto>().Add(foto);
        }

        public void IncluirFuncionario(Funcionario funcionario)
        {
            this.Db.Set<Funcionario>().Add(funcionario);
        }

        public void RemoverAtividade(Guid atividadeId)
        {
            this.Db.Set<Atividade>().Remove(this.Db.Set<Atividade>().AsNoTracking().FirstOrDefault(x => x.Id == atividadeId));
        }

        public void RemoverEquipe(Guid equipeId)
        {
            this.Db.Set<Equipe>().Remove(this.Db.Set<Equipe>().AsNoTracking().FirstOrDefault(x => x.Id == equipeId));
        }

        public void RemoverFuncionario(Guid funcionarioId)
        {
            this.Db.Set<Funcionario>().Remove(this.Db.Set<Funcionario>().AsNoTracking().FirstOrDefault(x => x.Id == funcionarioId));
        }

        public Empresa GerarGraficoAvaliacoesEventoDaEmpresa(Guid empresaId)
        {
            var sql = "SELECT em.Id, em.*, ev.Id, ev.*, a.ClienteId, a.* FROM Empresas em" +
                             " INNER JOIN Eventos ev" +
                             " on em.Id = ev.EmpresaId" +
                             " INNER JOIN Avaliacoes a" +
                             " on ev.Id = a.EventoId" +
                             " where em.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Evento, Avaliacao, Empresa>(sql,
                (em, ev, a) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(em.Id, out empresa))
                    {
                        lookup.Add(em.Id, empresa);
                        empresa = em;
                    }

                    if (ev != null)
                    {
                        if (a != null) ev.AdicionarAvaliacoesEvento(a);

                        empresa.AdicionarEventos(ev);
                    }


                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id,ClienteId");

            return emp.FirstOrDefault();
        }

        public Empresa GerarRelatorioPorCategoriasEventosFrequentadasDaEmpresa(Guid empresaId)
        {
            var sql = "SELECT em.Id, em.*, ev.Id, ev.*, c.Id, c.*, i.Id, i.*, ic.ClienteId, ic.* FROM Empresas em" +
                                      " INNER JOIN Eventos ev" +
                                      " on em.Id = ev.EmpresaId" +
                                      " INNER JOIN Categorias c" +
                                      " on c.Id = ev.CategoriaId" +
                                      " INNER JOIN Ingressos i" +
                                      " on ev.Id = i.EventoId" +
                                      " INNER JOIN IngressosComprados ic" +
                                      " on i.Id = ic.IngressoId" +
                                      " where em.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Evento, Categoria, Ingresso, IngressoComprado, Empresa>(sql,
                (em, ev, c, i, ic) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(em.Id, out empresa))
                    {
                        lookup.Add(em.Id, empresa);
                        empresa = em;
                    }

                    if (ev != null)
                    {
                        if (c != null) ev.AtribuirCategoriaEvento(c);

                        if (i != null)
                        {
                            if (ic != null) i.AdicionarIngressosComprados(ic);

                            ev.AdicionarIngressos(i);
                        }

                        em.AdicionarEventos(ev);
                    }

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id,Id,Id,ClienteId");

            return emp.FirstOrDefault();
        }

        public Empresa GerarRelatorioPorAtracoesProcuradasDaEmpresa(Guid empresaId)
        {
            var sql = "SELECT em.Id, em.*, ev.Id, ev.*, ae.AtracaoId, ae.*, a.Id, a.*, i.Id, i.*, ic.ClienteId, ic.* FROM Empresas em" +
                           " INNER JOIN Eventos ev" +
                           " on em.Id = ev.EmpresaId" +
                           " INNER JOIN AtracoesEventos ae" +
                           " on ev.Id = ae.EventoId" +
                           " INNER JOIN Atracoes a" +
                           " on ae.AtracaoId = a.Id" +
                           " INNER JOIN Ingressos i" +
                           " on ev.Id = i.EventoId" +
                           " INNER JOIN IngressosComprados ic" +
                           " on i.Id = ic.IngressoId" +
                           " where em.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Evento, AtracaoEvento, Atracao, Ingresso, IngressoComprado, Empresa>(sql,
                (em, ev, ae, a, i, ic) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(em.Id, out empresa))
                    {
                        lookup.Add(em.Id, empresa);
                        empresa = em;
                    }

                    if (ev != null)
                    {
                        if (ae != null)
                        {
                            if (a != null) ae.AtribuirAtracao(a);

                            ev.AdicionarAtracoesEvento(ae);
                        }

                        if (i != null)
                        {
                            if (ic != null) i.AdicionarIngressosComprados(ic);

                            ev.AdicionarIngressos(i);
                        }

                        em.AdicionarEventos(ev);
                    }

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id,AtracaoId,Id,Id,ClienteId");

            return emp.FirstOrDefault();
        }

        public Empresa GerarRelatorioPorCidadesMaisFrequentadasDaEmpresa(Guid empresaId)
        {
            var sql = "SELECT em.Id, em.*, ev.Id, ev.*, en.Id, en.*, c.Id, c.*, es.Id, es.*, i.Id, i.*, ic.ClienteId, ic.* FROM Empresas em" +
                             " INNER JOIN Eventos ev" +
                             " on em.Id = ev.EmpresaId" +
                             " INNER JOIN Enderecos en" +
                             " on en.Id = ev.EnderecoId" +
                             " INNER JOIN Cidades c" +
                             " on c.Id = en.CidadeId" +
                             " INNER JOIN Estados es" +
                             " on es.Id = c.EstadoId" +
                             " INNER JOIN Ingressos i" +
                             " on ev.Id = i.EventoId" +
                             " INNER JOIN IngressosComprados ic" +
                             " on i.Id = ic.IngressoId" +
                             " where em.Id = @eEmpresaId";

            Dictionary<Guid, Empresa> lookup = new Dictionary<Guid, Empresa>();

            var emp = this.Db.Database.GetDbConnection().
                Query<Empresa, Evento, Endereco, Cidade, Estado, Ingresso, IngressoComprado, Empresa>(sql,
                (em, ev, en, c, es, i, ic) =>
                {
                    Empresa empresa;

                    if (!lookup.TryGetValue(em.Id, out empresa))
                    {
                        lookup.Add(em.Id, empresa);
                        empresa = em;
                    }

                    if (ev != null)
                    {
                        if (en != null)
                        {
                            if (c != null)
                            {
                                if (es != null) c.AtribuirEstado(es);

                                en.AtribuirCidade(c);
                            }

                            ev.AtribuirEndereco(en);
                        }

                        if (i != null)
                        {
                            if (ic != null) i.AdicionarIngressosComprados(ic);

                            ev.AdicionarIngressos(i);
                        }

                        em.AdicionarEventos(ev);
                    }

                    return empresa;
                },
                new { eEmpresaId = empresaId },
                splitOn: "Id,Id,Id,Id,Id,Id,ClienteId");

            return emp.FirstOrDefault();
        }

        public Atividade BuscarAtividadePorId(Guid atividadeId)
        {
            var query = "SELECT * FROM ATIVIDADES WHERE ATIVIDADES.ID = @aAtividadeId";

            return Db.Database.GetDbConnection().QueryFirstOrDefault<Atividade>(query, new { aAtividadeId = atividadeId });
        }

        public Equipe BuscarEquipePorId(Guid equipeId)
        {
            var query = "SELECT * FROM EQUIPES WHERE EQUIPES.ID = @eEquipeId";

            return Db.Database.GetDbConnection().QueryFirstOrDefault<Equipe>(query, new { eEquipeId = equipeId });
        }

        public Funcionario BuscarFuncionarioPorId(Guid funcionarioId)
        {
            var query = "SELECT * FROM FUNCIONARIOS WHERE FUNCIONARIOS.ID = @fFuncionarioId";

            return Db.Database.GetDbConnection().QueryFirstOrDefault<Funcionario>(query, new { fFuncionarioId = funcionarioId });
        }

        public Empresa BuscarPorEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Email == email);
        }
    }
}
