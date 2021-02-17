using EventFinder.Domain.Core.Commands;
using EventFinder.Domain.Core.Events;
using System.Threading.Tasks;

namespace EventFinder.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}
