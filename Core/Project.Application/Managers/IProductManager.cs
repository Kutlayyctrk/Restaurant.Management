using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IProductManager:IManager<Product,ProductDTO>
    {
        Task<List<ProductDTO>> GetSellableProductsAsync(); //Sadece satılabilir ürünleri getirir
        Task<List<ProductDTO>> GetWithCategory(); //Ürünleri kategorileri ile birlikte getirir
        Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId);
        Task ReduceStockAfterSaleAsync(int productId, decimal orderQuantity, int orderDetailId);
        Task IncreaseStockAsync(int productId, decimal quantity);

    }
}
