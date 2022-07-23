using Ordering.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Interfaces
{
    public interface IOrderWriter
    {
        Task<string> CreateOrder(OrderDTO orderDTO);
        Task<bool> CancelOrder(string orderId);
        Task<bool> UpdateOrder(OrderDTO orderDTO);
    }
}
