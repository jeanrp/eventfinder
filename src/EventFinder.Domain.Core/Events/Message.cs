using MediatR;
using System;

namespace EventFinder.Domain.Core.Events
{
    public class Message : INotification
    {
        public string MessageType { get;protected set; }
        public Guid AggregateId { get;protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }

    }
}
