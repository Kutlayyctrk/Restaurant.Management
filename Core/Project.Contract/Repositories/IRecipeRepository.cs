using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IRecipeRepository:IRepository<Recipe>
    {
        Task<Recipe?> GetByProductIdAsync(int productId);
        Task<Recipe?> GetByIdWithItemsAsync(int id);
    }
}
