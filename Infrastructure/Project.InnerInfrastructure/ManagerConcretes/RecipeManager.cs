using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class RecipeManager : BaseManager<Recipe, RecipeDTO>, IRecipeManager
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeManager(IRecipeRepository recipeRepository, IMapper mapper, IValidator<RecipeDTO> recipeValidator)
            : base(recipeRepository, mapper, recipeValidator)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

       
        public override async Task<List<RecipeDTO>> GetAllAsync()
        {
            List<Recipe> recipes = await _recipeRepository.GetAllAsync();

            return recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                ProductId = r.ProductId,
                ProductName = r.Product?.ProductName,
                CategoryId = r.CategoryId,
                CategoryName = r.Category?.CategoryName,
                InsertedDate = r.InsertedDate,
                Status = r.Status,
                RecipeItem = r.RecipeItems.Select(ri => new RecipeItemDTO
                {
                    ProductId = ri.ProductId,
                    ProductName = ri.Product?.ProductName,
                    Quantity = ri.Quantity,
                    UnitId = ri.UnitId,
                    UnitName = ri.Unit?.UnitName
                }).ToList()
            }).ToList();
        }
    }
}