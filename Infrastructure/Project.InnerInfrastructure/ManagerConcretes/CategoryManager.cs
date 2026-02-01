using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class CategoryManager(ICategoryRepository categoryRepository, IMapper mapper, IValidator<CategoryDTO> categoryValidator) : BaseManager<Category, CategoryDTO>(categoryRepository, mapper, categoryValidator), ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<CategoryDTO>> GetRootCategoriesAsync()
        {
            List<Category> categories = await _categoryRepository.GetRootsAsync();
            return _mapper.Map<List<CategoryDTO>>(categories); ;
        }

        public async Task<List<CategoryDTO>> GetSubCategoriesByParentIdAsync(int parentId)
        {
            List<Category> categories = await _categoryRepository.GetByParentIdAsync(parentId);
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
