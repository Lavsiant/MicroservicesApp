using System.Threading.Tasks;
using System.Reflection;
using System;
using Ordering.Domain.Core;
using Ordering.Infrastructure.Interfaces.EventStore;
using Ordering.Infrastructure.Exceptions;
using Ordering.Domain.Core.Interfaces;

namespace Ordering.Domain.Persistence.EventStore
{
    public class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate>
        where TAggregate : AggregateBase, IAggregate
    {
        private readonly IEventStore eventStore;
        private readonly IScopedDomainEventPubSub publisher;

        public AggregateRepository(IEventStore eventStore, IScopedDomainEventPubSub publisher)
        {
            this.eventStore = eventStore;
            this.publisher = publisher;
        }

        public async Task<TAggregate> GetByIdAsync(string id)
        {
            try
            {
                var aggregate = CreateEmptyAggregate();
                IEventSourcingAggregate aggregatePersistence = aggregate;

                foreach (var @event in await eventStore.ReadEventsAsync(id))
                {
                    aggregatePersistence.ApplyEvent(@event);
                }
                return aggregate;
            }
            catch (EventStoreAggregateNotFoundException)
            {
                return null;
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException("Unable to access persistence layer", ex);
            }
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            try
            {
                IEventSourcingAggregate aggregatePersistence = aggregate;

                foreach (var @event in aggregatePersistence.GetUncommittedEvents())
                {
                    await eventStore.AppendEventAsync(@event);
                    await publisher.PublishAsync((dynamic)@event);
                }
                aggregatePersistence.ClearUncommittedEvents();
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException("Unable to access persistence layer", ex);
            }
        }

        private TAggregate CreateEmptyAggregate()
        {
            var type = typeof(TAggregate);
            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[0], new ParameterModifier[0]);
            if(constructor == null)
            {
                throw new MissingMethodException($"Cannot find a constructor for type {type.FullName}");
            }

            return (TAggregate)constructor.Invoke(new object[0]);
        }
    }
}
