using Ordering.Domain.ReadModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.DTO
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public long TotalCost { get; set; }
        public OrderStatus Status { get; set; }
        public AddressDTO BillingAddress { get; set; }
        public CardDTO PaymentCard { get; set; }
    }
}
