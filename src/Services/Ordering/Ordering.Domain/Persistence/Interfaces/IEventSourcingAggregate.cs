using Ordering.Domain.Core;
using Ordering.Infrastructure.Interfaces.EventStore;
using System.Collections.Generic;

namespace Ordering.Domain.Persistence
{
    public interface IEventSourcingAggregate
    {
        long Version { get; }
        void ApplyEvent(IDomainEvent @event);
        IEnumerable<IDomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
