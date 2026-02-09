using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryDTO> categories = await _categoryManager.GetActives();
            return Ok(ApiResponse<List<CategoryDTO>>.Ok(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryDTO category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound(ApiResponse<CategoryDTO>.Fail("Kategori bulunamadý."));

            return Ok(ApiResponse<CategoryDTO>.Ok(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO dto)
        {
            OperationStatus result = await _categoryManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Kategori oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<CategoryDTO>.Ok(dto, "Kategori baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO dto)
        {
            CategoryDTO original = await _categoryManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Kategori bulunamadý."));

            OperationStatus result = await _categoryManager.UpdateAsync(original, dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Kategori güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Kategori baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            OperationStatus result = await _categoryManager.SoftDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Kategori bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Kategori silindi (soft delete)."));
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(int id)
        {
            OperationStatus result = await _categoryManager.HardDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Kategori bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Kategori kalýcý olarak silindi."));
        }
    }
}
