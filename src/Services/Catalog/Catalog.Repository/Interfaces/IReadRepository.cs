using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Repository.Interfaces
{
    public interface IReadRepository<TEntity>
    {
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);

    }
}
