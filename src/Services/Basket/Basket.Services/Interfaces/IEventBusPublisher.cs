using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Interfaces
{
    public interface IEventBusPublisher
    {
        void SendEvent<T>(T @event);
    }
}
