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
    public class CategoryRepository(MyContext myContext) : BaseRepository<Category>(myContext), ICategoryRepository
    {
        private readonly MyContext _context = myContext;
        public async Task<List<Category>> GetByParentIdAsync(int parentId)
        {
            return await _context.Categories.Where(x => x.ParentCategoryId == parentId && x.Status != DataStatus.Deleted).ToListAsync();
        }

        public  async Task<List<Category>> GetRootsAsync()
        {
            return await _context.Categories.Where(x => x.ParentCategoryId == null && x.Status != DataStatus.Deleted).ToListAsync();
        }
    }
}
