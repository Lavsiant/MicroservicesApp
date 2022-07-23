using MongoDB.Driver;
using Ordering.Domain.ReadModel.Interfaces;
using Ordering.Domain.ReadModel.Model;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ReadModel.Repositories
{
    public class OrderRepository : MongoRepository<Order>, IOrderRepository
    {
        private const string ColumnName = "Orders";
        public OrderRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, ColumnName)
        {

        }
    }
}
