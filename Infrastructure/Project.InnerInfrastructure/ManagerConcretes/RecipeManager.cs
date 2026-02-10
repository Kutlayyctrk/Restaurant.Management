using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Application.Results;
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
        private readonly IUnitOfWork _unitOfWork;

        public RecipeManager(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<RecipeDTO> recipeValidator, ILogger<RecipeManager> logger)
            : base(recipeRepository, unitOfWork, mapper, recipeValidator, logger)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
                RecipeItems = r.RecipeItems.Select(ri => new RecipeItemDTO
                {
                    ProductId = ri.ProductId,
                    Quantity = ri.Quantity,
                    UnitId = ri.UnitId
                }).ToList()
            }).ToList();
        }

        public async Task<RecipeDTO?> GetByIdWithItemsAsync(int id)
        {
            Recipe? recipe = await _recipeRepository.GetByIdWithItemsAsync(id);
            if (recipe == null)
                return null;

            return new RecipeDTO
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                ProductId = recipe.ProductId,
                CategoryId = recipe.CategoryId,
                RecipeItems = recipe.RecipeItems.Select(ri => new RecipeItemDTO
                {
                    ProductId = ri.ProductId,
                    Quantity = ri.Quantity,
                    UnitId = ri.UnitId
                }).ToList()
            };
        }

        public async Task<RecipeDTO?> GetByProductIdAsync(int productId)
        {
            Recipe recipe = await _recipeRepository.GetByProductIdAsync(productId);

            if (recipe == null)
                return null;

            return new RecipeDTO
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                ProductId = recipe.ProductId,
                CategoryId = recipe.CategoryId,
                RecipeItems = recipe.RecipeItems.Select(ri => new RecipeItemDTO
                {
                    ProductId = ri.ProductId,
                    Quantity = ri.Quantity,
                    UnitId = ri.UnitId
                }).ToList()
            };
        }

        public override async Task<Result> UpdateAsync(RecipeDTO originalDto, RecipeDTO newDto)
        {
            Recipe originalEntity = await _recipeRepository.GetByIdAsync(originalDto.Id);
            if (originalEntity == null)
                return Result.Failure(OperationStatus.NotFound, "Reçete bulunamadı.");

            _mapper.Map(newDto, originalEntity);

            originalEntity.Status = Project.Domain.Enums.DataStatus.Updated;
            originalEntity.UpdatedDate = System.DateTime.Now;

            await _recipeRepository.UpdateAsync(originalEntity);
            await _unitOfWork.CommitAsync();

            return Result.Succeed("Reçete güncellendi.");
        }
    }
}