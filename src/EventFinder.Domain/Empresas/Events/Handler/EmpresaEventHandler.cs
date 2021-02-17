using MediatR;

namespace EventFinder.Domain.Empresas.Events.Handler
{
    public class EmpresaEventHandler :
        INotificationHandler<AtividadeAdicionadaEvent>,
        INotificationHandler<AtividadeAtualizadaEvent>,
        INotificationHandler<AtividadeExcluidaEvent>,
        INotificationHandler<EmpresaAdicionadaEvent>,
        INotificationHandler<EmpresaAtualizadaEvent>,
        INotificationHandler<EmpresaExcluidaEvent>,
        INotificationHandler<EquipeAdicionadaEvent>,
        INotificationHandler<EquipeAtualizadaEvent>,
        INotificationHandler<EquipeExcluidaEvent>,
        INotificationHandler<FuncionarioAdicionadoEvent>,
        INotificationHandler<FuncionarioAtualizadoEvent>,
        INotificationHandler<FuncionarioExcluidoEvent>
    {
        public void Handle(FuncionarioExcluidoEvent notification)
        {
            
        }

        public void Handle(FuncionarioAtualizadoEvent notification)
        {
            
        }

        public void Handle(FuncionarioAdicionadoEvent notification)
        {
        }

        public void Handle(EquipeExcluidaEvent notification)
        {
        }

        public void Handle(EquipeAdicionadaEvent notification)
        {
        }

        public void Handle(EmpresaExcluidaEvent notification)
        {
        }

        public void Handle(EmpresaAtualizadaEvent notification)
        {
        }

        public void Handle(EmpresaAdicionadaEvent notification)
        {
        }

        public void Handle(AtividadeExcluidaEvent notification)
        {
        }

        public void Handle(AtividadeAtualizadaEvent notification)
        {
        }

        public void Handle(AtividadeAdicionadaEvent notification)
        {
        }

        public void Handle(EquipeAtualizadaEvent notification)
        {
        }
    }
}
