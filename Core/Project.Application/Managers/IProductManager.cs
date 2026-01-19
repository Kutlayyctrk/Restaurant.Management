using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IProductManager:IManager<Product,ProductDTO>
    {
        Task<List<ProductDTO>> GetSellableProductsAsync();

    }
}
