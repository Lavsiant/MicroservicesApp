using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Interfaces
{
    public interface IReadRepository<TEntity, TId> where TEntity : class, IReadEntity<TId>
    {
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(TId id);
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
    }
}
