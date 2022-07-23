using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Interfaces
{
    public interface IReadEntity<T>
    {
        public T Id { get; }
    }
}
