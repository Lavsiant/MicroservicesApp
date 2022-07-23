using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;

namespace Ordering.Domain.Core
{
    public abstract class DomainEventBase : IDomainEvent, IEquatable<DomainEventBase>
    {
        protected DomainEventBase()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEventBase(string aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEventBase(string aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }

        public string AggregateId { get; private set; }

        public long AggregateVersion { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as DomainEventBase);
        }

        public bool Equals(DomainEventBase other)
        {
            return other != null &&
                   EventId.Equals(other.EventId);
        }

        public override int GetHashCode()
        {
            return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
        }

       //  public abstract IDomainEvent GetDomainWithAggregateId(string aggregateId, long aggregateVersion);
    }
}
