using Ordering.Domain.Common;
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
    public class OrderAggregate : AggregateBase
    {
        private OrderAggregate()
        {
        }

        public OrderAggregate(OrderDTO order)
        {
            UserName = order.Username;
            TotalCost = order.TotalCost;
            BillingAddress = order.BillingAddress;
            PaymentCard = order.PaymentCard;
            Status = OrderStatus.Processing;
            Id = IdGenerator.GetId();

            RaiseEvent(new OrderCreatedEvent(Id, UserName, TotalCost, Status, BillingAddress, PaymentCard));
        }

        public string UserName { get; private set; }
        public long TotalCost { get; private set; }
        public OrderStatus Status { get; set; }
        public AddressDTO BillingAddress { get; private set; }
        public CardDTO PaymentCard { get; private set; }

        public bool CancelOrder()
        {
            if(Status != OrderStatus.Completed)
            {
                RaiseEvent(new OrderCancelledEvent(Id));
                return true;
            }
            return false;
        }

        public bool UpdateOrder(OrderDTO order)
        {
            if (Status != OrderStatus.Completed)
            {
                RaiseEvent(new OrderUpdatedEvent(Id, order.TotalCost, order.BillingAddress, order.PaymentCard, OrderStatus.Processing));
                return true;
            }
            return false;
        }

        public override void ApplyEvent(IDomainEvent @event)
        {
            switch (@event)
            {
                case OrderCreatedEvent:
                    ApplyEvent((OrderCreatedEvent)@event);
                    break;
                case OrderCancelledEvent:
                    ApplyEvent((OrderCancelledEvent)@event);
                    break;
                case OrderUpdatedEvent:
                    ApplyEvent((OrderUpdatedEvent)@event);
                    break;
                default:
                    throw new NotImplementedException($"Aggregate's handler for '{@event.GetType()}' event not found");
            }
            Version = @event.AggregateVersion;
        }



        private void ApplyEvent(OrderCreatedEvent @event)
        {
            UserName = @event.UserName;
            TotalCost = @event.TotalCost;
            BillingAddress = @event.BillingAddress;
            PaymentCard = @event.PaymentCard;
            Status = @event.Status;
            Id = @event.AggregateId;
        }

        private void ApplyEvent(OrderCancelledEvent @event)
        {
            Status = OrderStatus.Cancelled;
        }

        public void ApplyEvent(OrderUpdatedEvent @event)
        {
            BillingAddress = @event.BillingAddress;
            PaymentCard = @event.PaymentCard;
            TotalCost = @event.TotalCost;
        }


    }
}
