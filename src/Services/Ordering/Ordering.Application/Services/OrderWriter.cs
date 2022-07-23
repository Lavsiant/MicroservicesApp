using Ordering.Application.Handlers;
using Ordering.Application.Interfaces;
using Ordering.Domain.Aggregates.OrderModule;
using Ordering.Domain.Core.Interfaces;
using Ordering.Domain.DTO;
using Ordering.Domain.Persistence;
using Ordering.Infrastructure.Interfaces.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public class OrderWriter : BaseEventService, IOrderWriter
    {
        private readonly IScopedDomainEventPubSub _subscriber;
        private readonly IAggregateRepository<OrderAggregate> _orderAggregateRepository;
        private readonly IEnumerable<IDomainEventHandler<OrderCreatedEvent>> _orderCreatedHandlers;
        private readonly IEnumerable<IDomainEventHandler<OrderCancelledEvent>> _orderCancelEvents;
        private readonly IEnumerable<IDomainEventHandler<OrderUpdatedEvent>> _orderUpdatedEvents;

        public OrderWriter(IScopedDomainEventPubSub subscriber, IAggregateRepository<OrderAggregate> orderAggregateRepository, IEnumerable<IDomainEventHandler<OrderCreatedEvent>> orderCreatedHandlers, IEnumerable<IDomainEventHandler<OrderCancelledEvent>> orderCancelEvents, IEnumerable<IDomainEventHandler<OrderUpdatedEvent>> orderUpdatedEvents)
        {
            _subscriber = subscriber!;
            _orderAggregateRepository = orderAggregateRepository!;
            _orderCreatedHandlers = orderCreatedHandlers;
            _orderCancelEvents = orderCancelEvents;
            _orderUpdatedEvents = orderUpdatedEvents;

            ((OrderUpdater)_orderCreatedHandlers.First()).TEST = 12312;
        }

        public async Task<string> CreateOrder(OrderDTO orderDTO)
        {
            var order = new OrderAggregate(orderDTO);

            _subscriber.Subscribe<OrderCreatedEvent>(async @event => await HandleEvent(@event, _orderCreatedHandlers));
            await _orderAggregateRepository.SaveAsync(order);
            return order.Id;
        }

        public async Task<bool> CancelOrder(string orderId)
        {
            var order = await _orderAggregateRepository.GetByIdAsync(orderId);
            var isOrderCancelled = order.CancelOrder();

            if(isOrderCancelled)
            {
                _subscriber.Subscribe<OrderCancelledEvent>(async @event => await HandleEvent(@event, _orderCancelEvents));
                await _orderAggregateRepository.SaveAsync(order);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateOrder(OrderDTO orderDTO)
        {
            var order = await _orderAggregateRepository.GetByIdAsync(orderDTO.Id);
            var isOrderUpdated = order.UpdateOrder(orderDTO);

            if(isOrderUpdated)
            {
                _subscriber.Subscribe<OrderUpdatedEvent>(async @event => await HandleEvent(@event, _orderUpdatedEvents));
                await _orderAggregateRepository.SaveAsync(order);
                return true;
            }
           
            return false;
        }


    }
}
