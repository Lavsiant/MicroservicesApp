using Ordering.Infrastructure.Models.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Interfaces.EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<IDomainEvent>> ReadEventsAsync(string id);

        Task AppendEventAsync(IDomainEvent @event);
    }
}
