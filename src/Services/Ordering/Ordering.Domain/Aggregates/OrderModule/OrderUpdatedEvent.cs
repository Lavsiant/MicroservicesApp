using Ordering.Domain.Core;
using Ordering.Domain.DTO;
using Ordering.Domain.ReadModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Aggregates.OrderModule
{
    public class OrderUpdatedEvent : DomainEventBase
    {
        private OrderUpdatedEvent() { }

        public OrderUpdatedEvent(string aggregateId, long totalCost, AddressDTO billingAddress, CardDTO paymentCard, OrderStatus status) : base(aggregateId)
        {
            TotalCost = totalCost;
            BillingAddress = billingAddress;
            PaymentCard = paymentCard;
            Status = status;
        }

        public long TotalCost { get; private set; }
        public AddressDTO BillingAddress { get; private set; }
        public CardDTO PaymentCard { get; private set; }
        public OrderStatus Status { get; private set; }
    }
}
