using EventFinder.Domain.Clientes;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Funcionarios;
using EventFinder.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EventFinder.Domain.Empresas.Repository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {

        void IncluirEquipe(Equipe equipe);
        void AlterarEquipe(Equipe equipe);
        void RemoverEquipe(Guid equipeId);        
        void IncluirAtividade(Atividade atividade);
        void AlterarAtividade(Atividade atividade);
        void RemoverAtividade(Guid atividadeId);
        void IncluirFuncionario(Funcionario funcionario);
        void AlterarFuncionario(Funcionario funcionario);
        void RemoverFuncionario(Guid funcionarioId);
        void IncluirFoto(Foto foto);
        void ExcluirFoto(Guid fotoId);
        Atividade BuscarAtividadePorId(Guid atividadeId);
        Equipe BuscarEquipePorId(Guid equipeId);
        Funcionario BuscarFuncionarioPorId(Guid funcionarioId);
        IEnumerable<Funcionario> BuscarFuncionariosPorEquipe(Guid equipeId);
        IEnumerable<Avaliacao> BuscarRelatorioAvaliacoesDosEventosDaEmpresa(Guid empresaId);
        IEnumerable<Atividade> BuscarAtividadesPorFuncionario(Guid funcionarioId);
        bool EnviarEmailParaFuncionariosComAtividades(IEnumerable<Funcionario> funcionario);
        bool EnviarSmsParaFuncionariosComAtividades(IEnumerable<Funcionario> funcionario);
        IEnumerable<Equipe> BuscarEquipesPorEmpresa(Guid empresaId);
        IEnumerable<Funcionario> BuscarFuncionariosPorEmpresa(Guid empresaId);
        IEnumerable<Foto> BuscarFotosPorEmpresa(Guid empresaId);
        IEnumerable<MensagemOrganizacaoEvento> BuscarMensagensPorEmpresa(Guid empresaId);
        IEnumerable<Atividade> BuscarAtividadesPorEmpresa(Guid empresaId);
        IEnumerable<Evento> BuscarEventosPorEmpresa(Guid empresaId);
        Empresa GerarGraficoAvaliacoesEventoDaEmpresa(Guid empresaId);
        Empresa GerarRelatorioPorCategoriasEventosFrequentadasDaEmpresa(Guid empresaId);
        Empresa GerarRelatorioPorAtracoesProcuradasDaEmpresa(Guid empresaId);
        Empresa GerarRelatorioPorCidadesMaisFrequentadasDaEmpresa(Guid empresaId);
        Empresa BuscarPorEmail(string email);


    }
}
