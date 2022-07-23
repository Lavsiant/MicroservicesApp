using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Exceptions
{
    [Serializable]
    public class EventStoreException : Exception
    {
        public EventStoreException() { }
        public EventStoreException(string message) : base(message) { }
        public EventStoreException(string message, Exception inner) : base(message, inner) { }
        protected EventStoreException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class EventStoreAggregateNotFoundException : EventStoreException
    {
        public EventStoreAggregateNotFoundException() { }
        public EventStoreAggregateNotFoundException(string message) : base(message) { }
        public EventStoreAggregateNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EventStoreAggregateNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class EventStoreCommunicationException : EventStoreException
    {
        public EventStoreCommunicationException() { }
        public EventStoreCommunicationException(string message) : base(message) { }
        public EventStoreCommunicationException(string message, Exception inner) : base(message, inner) { }
        protected EventStoreCommunicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    internal class AggregateNotProvidedException : EventStoreException
    {
        public AggregateNotProvidedException() { }

        public AggregateNotProvidedException(string message) : base(message) { }

        public AggregateNotProvidedException(string message, Exception inner) : base(message, inner) { }

        protected AggregateNotProvidedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
