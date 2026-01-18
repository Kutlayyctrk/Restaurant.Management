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
        public override async Task CreateAsync(Recipe entity)
        {
            await _context.Recipes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public  async Task UpdateAsync(Recipe entity)
        {
            Recipe existingRecipe = await _context.Recipes
                .Include(r => r.RecipeItems)
                .FirstOrDefaultAsync(r => r.Id == entity.Id);

            if (existingRecipe == null) return;

           
            _context.Entry(existingRecipe).CurrentValues.SetValues(entity);

            _context.RecipeItems.RemoveRange(existingRecipe.RecipeItems);

            
            foreach (RecipeItem item in entity.RecipeItems)
            {
                existingRecipe.RecipeItems.Add(item);
            }

            await _context.SaveChangesAsync();
        }

    }
}