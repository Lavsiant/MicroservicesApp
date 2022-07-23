using Ordering.Application.Interfaces;
using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public abstract class BaseEventService
    {
        protected async Task HandleEvent<T>(T @event, IEnumerable<IDomainEventHandler<T>> handlers) where T : IDomainEvent
        {
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
