using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.Persistance.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.Repositories
{
    public class ProductRepository(MyContext myContext) : BaseRepository<Product>(myContext), IProductRepository
    {
        private readonly MyContext _context = myContext;

        public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == categoryId && x.Status != DataStatus.Deleted).ToListAsync();
        }

        public async Task<Product> GetProductWithRecipeAsync(int productId)
        {
            return await _context.Products
              .Include(p => p.Recipe) 
                  .ThenInclude(r => r.RecipeItems) 
                      .ThenInclude(ri => ri.Product) 
              .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> GetSellableProductsAsync()
        {
            return await _context.Products
                .Where(p => p.IsSellable)
                .ToListAsync();
        }

        public async Task<List<Product>> GetWithCategory()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

    }
}
