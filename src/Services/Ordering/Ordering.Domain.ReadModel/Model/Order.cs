using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ordering.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ReadModel.Model
{
    public class Order : IReadEntity<string>
    {
        public string Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public string Username { get; set; }
        public long TotalCost { get; set; }
        public OrderStatus Status { get; set; }
        public Address BillingAddress { get; set; }
        public Card PaymentCard { get; set; }
    }
}
