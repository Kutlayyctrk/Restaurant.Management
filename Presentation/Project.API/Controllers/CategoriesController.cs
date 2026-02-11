using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Application.Managers;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMemoryCache _cache;

        public CategoriesController(ICategoryManager categoryManager, IMemoryCache cache)
        {
            _categoryManager = categoryManager;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryDTO> categories = await _categoryManager.GetActives();
            return Ok(ApiResponse<List<CategoryDTO>>.Ok(categories));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            string cacheKey = $"categories_page_{page}_size_{pageSize}";

            if (!_cache.TryGetValue(cacheKey, out PagedResult<CategoryDTO> pagedResult))
            {
                pagedResult = await _categoryManager.GetPagedAsync(page, pageSize);
                _cache.Set(cacheKey, pagedResult, TimeSpan.FromMinutes(5));
            }

            return Ok(ApiResponse<PagedResult<CategoryDTO>>.Ok(pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryDTO category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound(ApiResponse<CategoryDTO>.Fail("Kategori bulunamadı."));

            return Ok(ApiResponse<CategoryDTO>.Ok(category));
        }

        [HttpGet("roots")]
        public async Task<IActionResult> GetRootCategories()
        {
            List<CategoryDTO> categories = await _categoryManager.GetRootCategoriesAsync();
            return Ok(ApiResponse<List<CategoryDTO>>.Ok(categories));
        }

        [HttpGet("{parentId}/subcategories")]
        public async Task<IActionResult> GetSubCategories(int parentId)
        {
            List<CategoryDTO> categories = await _categoryManager.GetSubCategoriesByParentIdAsync(parentId);
            return Ok(ApiResponse<List<CategoryDTO>>.Ok(categories));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO dto)
        {
            Result result = await _categoryManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Kategori oluşturulamadı. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<CategoryDTO>.Ok(dto, "Kategori başarıyla oluşturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO dto)
        {
            CategoryDTO original = await _categoryManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Kategori bulunamadı."));

            Result result = await _categoryManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Kategori güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Kategori başarıyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _categoryManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Kategori bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Kategori silindi (soft delete)."));
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(int id)
        {
            Result result = await _categoryManager.HardDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Kategori bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Kategori kalıcı olarak silindi."));
        }
    }
}
