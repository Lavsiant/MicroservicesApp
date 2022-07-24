using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Core.Interfaces
{
    public interface IEventBusConsumer
    {
        void InitializeSubscriber();
    }
}
