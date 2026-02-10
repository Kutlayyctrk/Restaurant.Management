using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Application.Managers;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeManager _recipeManager;

        public RecipesController(IRecipeManager recipeManager)
        {
            _recipeManager = recipeManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RecipeDTO> recipes = await _recipeManager.GetActives();
            return Ok(ApiResponse<List<RecipeDTO>>.Ok(recipes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RecipeDTO recipe = await _recipeManager.GetByIdAsync(id);
            if (recipe == null)
                return NotFound(ApiResponse<RecipeDTO>.Fail("Reçete bulunamadý."));

            return Ok(ApiResponse<RecipeDTO>.Ok(recipe));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecipeDTO dto)
        {
            Result result = await _recipeManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Reçete oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<RecipeDTO>.Ok(dto, "Reçete baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecipeDTO dto)
        {
            RecipeDTO original = await _recipeManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Reçete bulunamadý."));

            Result result = await _recipeManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Reçete güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Reçete baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _recipeManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Reçete bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Reçete silindi (soft delete)."));
        }
    }
}
