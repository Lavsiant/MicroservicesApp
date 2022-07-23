using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Core.Interfaces
{
    public interface IScopedDomainEventPubSub
    {
        Task PublishAsync<T>(T publishedEvent) where T : IDomainEvent;
        void Subscribe<T>(Action<T> handler) where T : IDomainEvent;
        void Subscribe<T>(Func<T, Task> handler) where T : IDomainEvent;
    }
}
