using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Interfaces.EventStore
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        string AggregateId { get; }
        long AggregateVersion { get; }
    }
}
