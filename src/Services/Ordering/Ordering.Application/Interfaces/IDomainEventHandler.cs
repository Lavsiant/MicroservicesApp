using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Interfaces
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent    
    {
        Task HandleAsync(TEvent @event);
    }
}
