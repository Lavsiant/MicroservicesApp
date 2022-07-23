using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetProductByCategory(string category);
    }
}
