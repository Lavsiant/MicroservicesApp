using AutoMapper;
using Ordering.Application.Interfaces;
using Ordering.Domain.Aggregates.OrderModule;
using Ordering.Domain.ReadModel.Interfaces;
using Ordering.Domain.ReadModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class OrderUpdater : 
        IDomainEventHandler<OrderCreatedEvent>, 
        IDomainEventHandler<OrderCancelledEvent>,
        IDomainEventHandler<OrderUpdatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public int TEST { get; set; }
        public OrderUpdater(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(OrderCreatedEvent @event)
        {
            var order = _mapper.Map<Order>(@event);
            order.Id = @event.AggregateId;

            await _orderRepository.AddAsync(order);
            
        }

        public async Task HandleAsync(OrderCancelledEvent @event)
        {
            var order = _orderRepository.GetById(@event.AggregateId);
            // ???
            order.Status = OrderStatus.Cancelled;
            await _orderRepository.UpdateAsync(order);
        }

        public async Task HandleAsync(OrderUpdatedEvent @event)
        {
            var order = _orderRepository.GetById(@event.AggregateId);
            // ???

            order.Status = @event.Status;
            order.TotalCost = @event.TotalCost;
            order.PaymentCard = _mapper.Map<Card>(@event.PaymentCard);
            order.BillingAddress = _mapper.Map<Address>(@event.BillingAddress);

            await _orderRepository.UpdateAsync(order);
        }
    }
}
