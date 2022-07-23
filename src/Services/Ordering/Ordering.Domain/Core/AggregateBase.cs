using Ordering.Infrastructure.Interfaces.EventStore;
using Ordering.Domain.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Core
{
    public abstract class AggregateBase : IAggregate, IEventSourcingAggregate
    {
        public const long NewAggregateVersion = -1;

        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

        public string Id { get; protected set;  }

        public long Version { get; set; } = NewAggregateVersion;

        public abstract void ApplyEvent(IDomainEvent @event);

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent: DomainEventBase
        {    
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                @event.AggregateVersion = Version + 1;

                ApplyEvent(@event);            
                _uncommittedEvents.Add(@event);
            }
        }
    }
}
