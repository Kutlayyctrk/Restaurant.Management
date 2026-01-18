using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Application.Enums;
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
                CategoryId = r.CategoryId,
                RecipeItem = r.RecipeItems.Select(ri => new RecipeItemDTO
                {
                    ProductId = ri.ProductId,
                    Quantity = ri.Quantity,
                    UnitId = ri.UnitId
                }).ToList()
            }).ToList();
        }
        public async Task<OperationStatus> UpdateAsync(RecipeDTO dto)
        {
            Recipe entity = _mapper.Map<Recipe>(dto);
            await _recipeRepository.UpdateAsync(entity);
            return OperationStatus.Success;
        }


    }
}