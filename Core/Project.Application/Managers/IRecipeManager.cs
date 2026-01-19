using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IRecipeManager : IManager<Recipe, RecipeDTO>
    {
        Task<List<RecipeDTO>> GetAllAsync();
        Task<RecipeDTO?> GetByProductIdAsync(int productId);
        Task<RecipeDTO?> GetByIdWithItemsAsync(int id);

    }
}
