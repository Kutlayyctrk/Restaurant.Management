using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.Repositories
{
    public class MenuProductRepository(MyContext myContext) : BaseRepository<MenuProduct>(myContext), IMenuProductRepository
    {
        private readonly MyContext _context = myContext;
        public  async Task<List<MenuProduct>> GetWithMenuAndProduct()
        { 
           return await _context.MenuProducts.Include(mp => mp.Menu)
                .Include(mp => mp.Product)
                .Include(mp=> mp.Product.Category)
                .ToListAsync();
        }
    }
}
