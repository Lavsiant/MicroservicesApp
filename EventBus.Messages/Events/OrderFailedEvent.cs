using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class OrderFailedEvent
    {
        public string Username { get; set; }
        public string Error { get; set; }
    }
}
