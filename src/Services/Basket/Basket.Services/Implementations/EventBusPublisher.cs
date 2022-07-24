using Basket.Services.Interfaces;
using EventBus.Messages;
using EventBus.Messages.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Implementations
{
    public class EventBusPublisher : IEventBusPublisher
    {
        private readonly IConnection _connection;

        private readonly IModel _busChannel;

        public EventBusPublisher(IConnection connection)
        {
            _connection = connection;
            _busChannel = _connection.CreateModel();
        }

        public void SendEvent<T>(T @event)
        {
            if (@event == null)
            {
                return;
            }

            switch (@event)
            {
                case CheckoutEvent:
                    SendCheckoutEvent(@event as CheckoutEvent);
                    break;
                default:
                    return;
            }
        }

        private void SendCheckoutEvent(CheckoutEvent @event)
        {
            _busChannel.ExchangeDeclare(exchange: EventBusConstants.OrderExchange, type: "direct");

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            _busChannel.BasicPublish(exchange: EventBusConstants.OrderExchange,
                                 routingKey: EventBusConstants.CheckoutRoute,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
