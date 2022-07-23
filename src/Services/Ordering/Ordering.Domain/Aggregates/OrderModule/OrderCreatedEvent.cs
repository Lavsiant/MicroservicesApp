using Ordering.Domain.Core;
using Ordering.Domain.DTO;
using Ordering.Domain.ReadModel.Model;
using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Aggregates.OrderModule
{
    public class OrderCreatedEvent : DomainEventBase
    {
        private OrderCreatedEvent() { }

        public OrderCreatedEvent(string aggregateId, string userName, long totalCost, OrderStatus status,  AddressDTO billingAddress, CardDTO paymentCard) : base(aggregateId)
        {
            UserName = userName;
            TotalCost = totalCost;
            BillingAddress = billingAddress;
            PaymentCard = paymentCard;
            Status = status;
        }

        public string UserName { get; private set; }
        public long TotalCost { get; private set; }
        public OrderStatus Status { get; set; }
        public AddressDTO BillingAddress { get; private set; }
        public CardDTO PaymentCard { get; private set; }
    }
}
