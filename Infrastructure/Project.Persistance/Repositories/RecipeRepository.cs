using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Persistance.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        private readonly MyContext _context;

        public RecipeRepository(MyContext myContext) : base(myContext)
        {
            _context = myContext;
        }

        public override async Task<List<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.Product)              
                .Include(r => r.Category)             
                .Include(r => r.RecipeItems)          
                    .ThenInclude(ri => ri.Product)    
                .Include(r => r.RecipeItems)
                    .ThenInclude(ri => ri.Unit)       
                .ToListAsync();
        }
    }
}