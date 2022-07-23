using Ordering.Domain.Core;
using System.Threading.Tasks;
using System;

namespace Ordering.Domain.Persistence
{
    public interface IAggregateRepository<TAggregate>
        where TAggregate: IAggregate
    {
        Task<TAggregate> GetByIdAsync(string id);

        Task SaveAsync(TAggregate aggregate);
    }
}