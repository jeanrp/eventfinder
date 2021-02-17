using EventFinder.Domain.Core.Events;
using System;

namespace EventFinder.Domain.Core.Commands
{
    public class Command : Message
    {
        public DateTime Timestamp { get;private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
