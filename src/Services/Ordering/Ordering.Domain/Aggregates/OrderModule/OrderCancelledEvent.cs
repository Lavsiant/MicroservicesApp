using Ordering.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Aggregates.OrderModule
{
    public class OrderCancelledEvent : DomainEventBase
    {
        private OrderCancelledEvent() { }

        public OrderCancelledEvent(string aggregateId) : base(aggregateId) { }
    }
}
