using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Core.Utils;
using EventFinder.Domain.Empresas.Events;
using EventFinder.Domain.Empresas.Repository;
using EventFinder.Domain.Funcionarios;
using EventFinder.Domain.Handlers;
using EventFinder.Domain.Interfaces;
using EventFinder.Domain.Usuarios;
using EventFinder.Domain.Usuarios.Repository;
using MediatR;
using System;

namespace EventFinder.Domain.Empresas.Commands.Handler
{
    public class EmpresaCommandHandler :
        CommandHandler,
        INotificationHandler<EditarAtividadeCommand>,
        INotificationHandler<EditarEmpresaCommand>,
        INotificationHandler<EditarEquipeCommand>,
        INotificationHandler<EditarFuncionarioCommand>,
        INotificationHandler<ExcluirAtividadeCommand>,
        INotificationHandler<ExcluirEmpresaCommand>,
        INotificationHandler<ExcluirEquipeCommand>,
        INotificationHandler<ExcluirFuncionarioCommand>,
        INotificationHandler<IncluirAtividadeCommand>,
        INotificationHandler<IncluirEmpresaCommand>,
        INotificationHandler<IncluirEquipeCommand>,
        INotificationHandler<IncluirFuncionarioCommand>

    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _repository;
        private readonly IMediatorHandler _mediator;

        public EmpresaCommandHandler(
            IUsuarioRepository usuarioRepository,
            IEmpresaRepository repository,
            IUnitOfWork uow,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _repository = repository;
            _mediator = mediator;
        }

        public void Handle(IncluirFuncionarioCommand cmd)
        {
            Funcionario funcionario = new Funcionario(cmd.Id, cmd.Nome, cmd.Email, cmd.Telefone, cmd.Facebook, cmd.Cargo, cmd.Sexo, cmd.EmpresaId, cmd.EquipeId);

            if (!funcionario.IsValid())
            {
                NotificarValidacoesErro(funcionario.ValidationResult);
                return;
            }

            _repository.IncluirFuncionario(funcionario);

            if (Commit())
                  _mediator.PublicarEvento(new FuncionarioAdicionadoEvent(funcionario.Id, funcionario.Nome, funcionario.Email, funcionario.Telefone, funcionario.Facebook, funcionario.Cargo, funcionario.Sexo, funcionario.EmpresaId, funcionario.EquipeId));
        }


        public Funcionario FuncionarioExistente(Guid funcionarioId, string messageType)
        {
            var funcionario = _repository.BuscarFuncionarioPorId(funcionarioId);


            if (funcionario != null) return funcionario;


            _mediator.PublicarEvento(new DomainNotification(messageType, "O funcionario não foi encontrado"));
            return null;

        }


        public Empresa EmpresaExistente(Guid empresaId, string messageType)
        {
            var empresa = _repository.ObterPorId(empresaId);


            if (empresa != null) return empresa;


            _mediator.PublicarEvento(new DomainNotification(messageType, "A empresa não foi encontrada"));
            return null;
        }


        public Equipe EquipeExistente(Guid equipeId, string messageType)
        {
            var equipe = _repository.BuscarEquipePorId(equipeId);


            if (equipe != null) return equipe;


            _mediator.PublicarEvento(new DomainNotification(messageType, "A equipe não foi encontrada"));
            return null;
        }

        public Atividade AtividadeExistente(Guid atividadeId, string messageType)
        {
            var atividade = _repository.BuscarAtividadePorId(atividadeId);


            if (atividade != null) return atividade;


            _mediator.PublicarEvento(new DomainNotification(messageType, "A atividade não foi encontrada"));
            return null;
        }


        public void Handle(IncluirEquipeCommand cmd)
        {
            Equipe equipe = new Equipe(cmd.Id, cmd.Nome, cmd.Descricao);

            if (!equipe.IsValid())
            {
                NotificarValidacoesErro(equipe.ValidationResult);
                return;
            }

            _repository.IncluirEquipe(equipe);

            if (Commit())
                _mediator.PublicarEvento(new EquipeAdicionadaEvent(equipe.Id, equipe.Nome, equipe.Descricao, equipe.EmpresaId));            
        }               
        

        public void Handle(IncluirEmpresaCommand cmd)          
        {
            Empresa empresa = new Empresa(cmd.Id, cmd.RazaoSocial, cmd.Email, StringHelper.RemoverCaracteresEspeciais(cmd.Telefone), cmd.Facebook, StringHelper.RemoverCaracteresEspeciais(cmd.Cnpj), cmd.EnderecoId);

            Usuario usuario = new Usuario(Guid.NewGuid(), cmd.RazaoSocial, DateTime.Now, cmd.Email, cmd.Senha, empresa, empresa.Id);

            if (!usuario.IsValid())
            {
                NotificarValidacoesErro(usuario.ValidationResult);
                return;
            }

            _usuarioRepository.Adicionar(usuario);             

            if (Commit())
                _mediator.PublicarEvento(new EmpresaAdicionadaEvent(empresa.Id,empresa.RazaoSocial,empresa.Email,empresa.Telefone,empresa.Facebook,empresa.Cnpj,empresa.EnderecoId));                                                                                            
        }

        public void Handle(IncluirAtividadeCommand cmd)
        {
            Atividade atividade = new Atividade(cmd.Id, cmd.Descricao, cmd.Nome, cmd.DataHoraInicio, cmd.DataHoraFim, cmd.EmpresaId);


            if (!atividade.IsValid())
            {
                NotificarValidacoesErro(atividade.ValidationResult);
                return;
            }

            _repository.IncluirAtividade(atividade);

            if (Commit())
                _mediator.PublicarEvento(new AtividadeAdicionadaEvent(atividade.Id,atividade.Descricao,atividade.Nome,atividade.DataHoraInicio,atividade.DataHoraFim,atividade.EmpresaId));
        }

        public void Handle(ExcluirFuncionarioCommand cmd)
        {
            Funcionario funcionario =  FuncionarioExistente(cmd.Id, cmd.MessageType);

            if (funcionario != null)
            {
                _repository.RemoverFuncionario(cmd.Id);

                if (Commit())
                    _mediator.PublicarEvento(new FuncionarioExcluidoEvent(funcionario.Id));
            }
        }

        public void Handle(ExcluirEquipeCommand cmd)
        {
            Equipe equipe = EquipeExistente(cmd.Id, cmd.MessageType);

            if (equipe != null)
            {
                _repository.Remover(cmd.Id);

                if (Commit())
                    _mediator.PublicarEvento(new EquipeExcluidaEvent(equipe.Id));
            }
        }

        public void Handle(ExcluirEmpresaCommand cmd)
        {
            Empresa empresa = EmpresaExistente(cmd.Id, cmd.MessageType);

            if (empresa != null)
            {
                _repository.Remover(cmd.Id);

                if (Commit())
                    _mediator.PublicarEvento(new EmpresaExcluidaEvent(empresa.Id));
            }
        }

        public void Handle(ExcluirAtividadeCommand cmd)
        {
            Atividade atividade = AtividadeExistente(cmd.Id, cmd.MessageType);

            if (atividade  != null)
            {
                _repository.Remover(cmd.Id);

                if (Commit())
                    _mediator.PublicarEvento(new EmpresaExcluidaEvent(atividade.Id));
            }
        }

        public void Handle(EditarFuncionarioCommand cmd)
        {
            Funcionario funcionario = FuncionarioExistente(cmd.Id, cmd.MessageType);

            if (funcionario != null)
            {
                funcionario.AtualizarFuncionario(cmd.Nome, cmd.Email, cmd.Telefone, cmd.Facebook, cmd.Cargo, cmd.Sexo, cmd.EquipeId);

                if (!funcionario.IsValid())
                {
                    NotificarValidacoesErro(funcionario.ValidationResult);
                    return;
                }

                _repository.AlterarFuncionario(funcionario);

                if (Commit())
                    _mediator.PublicarEvento(new FuncionarioAtualizadoEvent(funcionario.Id, funcionario.Nome, funcionario.Email, funcionario.Telefone, funcionario.Facebook, funcionario.Cargo, funcionario.Sexo, funcionario.EquipeId));

            }
        }

        public void Handle(EditarEquipeCommand cmd)
        {
            Equipe equipe = EquipeExistente(cmd.Id, cmd.MessageType);

            if (equipe != null)
            {
                equipe.AtualizarEquipe(cmd.Nome, cmd.Descricao);

                if (!equipe.IsValid())
                {
                    NotificarValidacoesErro(equipe.ValidationResult);
                    return;
                }

                _repository.AlterarEquipe(equipe);

                if (Commit())
                    _mediator.PublicarEvento(new EquipeAtualizadaEvent(equipe.Id,equipe.Nome,equipe.Descricao));

            }
        }

        public void Handle(EditarEmpresaCommand cmd)
        {
            Empresa empresa = EmpresaExistente(cmd.Id, cmd.MessageType);

            if (empresa != null)
            {
                empresa.AtualizarEmpresa(cmd.RazaoSocial, cmd.Email, cmd.Telefone, cmd.Facebook, cmd.Cnpj);

                if (!empresa.IsValid())
                {
                    NotificarValidacoesErro(empresa.ValidationResult);
                    return;
                }
                
                Usuario usuario = _usuarioRepository.ObterPorId(empresa.Id);
                usuario.AtualizarUsuario(cmd.RazaoSocial, cmd.Email, cmd.Senha);
                _repository.Atualizar(empresa);

                if (Commit())
                    _mediator.PublicarEvento(new EmpresaAtualizadaEvent(empresa.Id,empresa.RazaoSocial,empresa.Email,empresa.Telefone,empresa.Facebook,empresa.Cnpj));

            }
        }

        public void Handle(EditarAtividadeCommand cmd)
        {
            Atividade atividade = AtividadeExistente(cmd.Id, cmd.MessageType);

            if (atividade != null)
            {
                atividade.AtualizarAtividade(cmd.Descricao,cmd.Nome,cmd.DataHoraInicio,cmd.DataHoraFim);

                if (!atividade.IsValid())
                {
                    NotificarValidacoesErro(atividade.ValidationResult);
                    return;
                }

                _repository.AlterarAtividade(atividade);

                if (Commit())
                    _mediator.PublicarEvento(new AtividadeAtualizadaEvent(atividade.Id,atividade.Descricao, atividade.Nome, atividade.DataHoraInicio,atividade.DataHoraFim, atividade.FuncionarioId));

            }
        }
    }
}
