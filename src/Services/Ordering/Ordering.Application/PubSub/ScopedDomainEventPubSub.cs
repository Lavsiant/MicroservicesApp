using Ordering.Domain.Core.Interfaces;
using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.PubSub
{
    public class ScopedDomainEventPubSub : IScopedDomainEventPubSub
    {
        public Dictionary<Type, List<object>> Handlers { get; private set; } = new Dictionary<Type, List<object>>();

        public ScopedDomainEventPubSub()
        {
        }


        public void Subscribe<T>(Action<T> handler) where T : IDomainEvent
        {
            GetHandlersOf<T>().Add(handler);
        }

        public void Subscribe<T>(Func<T, Task> handler) where T : IDomainEvent
        {
            GetHandlersOf<T>().Add(handler);
        }

        public async Task PublishAsync<T>(T publishedEvent) where T : IDomainEvent
        {
            foreach (var handler in GetHandlersOf<T>())
            {
                switch (handler)
                {
                    case Action<T> action:
                        action(publishedEvent);
                        break;
                    case Func<T, Task> action:
                        await action(publishedEvent);
                        break;
                    default:
                        break;
                }
            }
        }

        private ICollection<object> GetHandlersOf<T>()
        {
            
            return Handlers.GetValueOrDefault(typeof(T)) ?? (Handlers[typeof(T)] = new List<object>());
        }
    }
}

