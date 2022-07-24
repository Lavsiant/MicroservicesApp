using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public static class EventBusConstants
    {
        public const string OrderExchange = "order-exchange";

        public const string CheckoutRoute = "checkout";
        public const string OrderCompletedRoute = "order-completed";
        public const string OrderFailedRoute = "order-failed";
    }
}
