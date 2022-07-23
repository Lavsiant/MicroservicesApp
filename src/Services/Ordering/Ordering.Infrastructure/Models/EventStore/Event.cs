using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Models.EventStore
{
    internal class Event
    {
        public Event()
        {

        }

        public Event(Guid id, string data, long version, DateTimeOffset createdAt, string eventTypeName, string aggregateId)
        {
            Id = id;
            Data = data;
            Version = version;
            CreatedAt = createdAt;
            EventTypeName = eventTypeName;
            AggregateId = aggregateId;
        }

        public Guid Id { get; set; }
        public string Data { get; set; }
        public long Version { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string EventTypeName { get; set; }
        public string AggregateId { get; set; }
    }
}
