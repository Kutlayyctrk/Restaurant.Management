using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<List<Product>> GetSellableProductsAsync();
        Task<List<Product>> GetWithCategory();
        Task<List<Product>> GetByCategoryIdAsync(int categoryId);
    }
}
