using AutoMapper;
using EventBus.Messages;
using EventBus.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ordering.Application.Interfaces;
using Ordering.Domain.Core.Interfaces;
using Ordering.Domain.DTO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.EventBusConsumers
{
    public class CheckoutEventConsumer : IEventBusConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public CheckoutEventConsumer(IConnection connection, IMapper mapper, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public void InitializeSubscriber()
        {
            _channel.ExchangeDeclare(exchange: EventBusConstants.OrderExchange, type: "direct");
            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                             exchange: EventBusConstants.OrderExchange,
                             routingKey: EventBusConstants.CheckoutRoute);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var checkoutEvent = JsonConvert.DeserializeObject<CheckoutEvent>(json);
                var orderDTO = _mapper.Map<OrderDTO>(checkoutEvent);

                using (var scope = _serviceProvider.CreateScope())
                {
                    if (scope.ServiceProvider.GetRequiredService(typeof(IOrderWriter)) is IOrderWriter orderWriter)
                    {
                        await orderWriter.CreateOrder(orderDTO);
                    }
                }

              

            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
