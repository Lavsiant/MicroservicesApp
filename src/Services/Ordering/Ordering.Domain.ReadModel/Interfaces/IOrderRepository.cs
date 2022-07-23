using Ordering.Domain.ReadModel.Model;
using Ordering.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ReadModel.Interfaces
{
    public interface IOrderRepository : IRepository<Order, string>
    {
    }
}
