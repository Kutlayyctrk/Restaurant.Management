using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class MenusController : ControllerBase
    {
        private readonly IMenuManager _menuManager;
        private readonly IMenuProductManager _menuProductManager;

        public MenusController(IMenuManager menuManager, IMenuProductManager menuProductManager)
        {
            _menuManager = menuManager;
            _menuProductManager = menuProductManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<MenuDTO> menus = await _menuManager.GetActives();
            return Ok(ApiResponse<List<MenuDTO>>.Ok(menus));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            MenuDTO menu = await _menuManager.GetByIdAsync(id);
            if (menu == null)
                return NotFound(ApiResponse<MenuDTO>.Fail("Menü bulunamadı."));

            return Ok(ApiResponse<MenuDTO>.Ok(menu));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuDTO dto)
        {
            Result result = await _menuManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Menü oluşturulamadı. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<MenuDTO>.Ok(dto, "Menü başarıyla oluşturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuDTO dto)
        {
            MenuDTO original = await _menuManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Menü bulunamadı."));

            Result result = await _menuManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Menü güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Menü başarıyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _menuManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Menü bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Menü silindi (soft delete)."));
        }

        // ---- Menü Ürünleri ----

        [HttpGet("products")]
        public async Task<IActionResult> GetAllMenuProducts()
        {
            List<MenuProductDTO> menuProducts = await _menuProductManager.GetWithMenuAndProduct();
            return Ok(ApiResponse<List<MenuProductDTO>>.Ok(menuProducts));
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetMenuProductById(int id)
        {
            MenuProductDTO menuProduct = await _menuProductManager.GetByIdAsync(id);
            if (menuProduct == null)
                return NotFound(ApiResponse<MenuProductDTO>.Fail("Menü ürünü bulunamadı."));

            return Ok(ApiResponse<MenuProductDTO>.Ok(menuProduct));
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateMenuProduct([FromBody] MenuProductDTO dto)
        {
            Result result = await _menuProductManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Menü ürünü oluşturulamadı. Durum: {result}"));

            return Ok(ApiResponse<MenuProductDTO>.Ok(dto, "Menü ürünü başarıyla oluşturuldu."));
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> SoftDeleteMenuProduct(int id)
        {
            Result result = await _menuProductManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Menü ürünü bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Menü ürünü silindi (soft delete)."));
        }
    }
}
