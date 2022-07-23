using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public static class IdGenerator
    {
        public static string GetId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
