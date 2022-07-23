using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ReadModel.Model
{
    public enum OrderStatus
    {
        Processing = 1,
        Completed = 2,
        Declined = 3,
        Cancelled = 4,
    }
}
