using Catalog.Models;
using Catalog.Repository.Interfaces;
using Catalog.Repository.Repositories.Base;
using MongoDB.Driver;

namespace Catalog.Repository.Repositories
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        private const string _collectionName = "Products";
        public ProductRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, _collectionName)
        {
            ProductContextSeed.SeedData(MongoCollection);
        }

        public async Task<ICollection<Product>> GetProductByCategory(string category)
        {
            return await MongoCollection.Find(x => x.Category == category).ToListAsync();
        }
    }
}
