using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;

namespace Project.API.Controllers
{
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
                return NotFound(ApiResponse<MenuDTO>.Fail("Menü bulunamadý."));

            return Ok(ApiResponse<MenuDTO>.Ok(menu));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuDTO dto)
        {
            OperationStatus result = await _menuManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Menü oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<MenuDTO>.Ok(dto, "Menü baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuDTO dto)
        {
            MenuDTO original = await _menuManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Menü bulunamadý."));

            OperationStatus result = await _menuManager.UpdateAsync(original, dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Menü güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Menü baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            OperationStatus result = await _menuManager.SoftDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Menü bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Menü silindi (soft delete)."));
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
                return NotFound(ApiResponse<MenuProductDTO>.Fail("Menü ürünü bulunamadý."));

            return Ok(ApiResponse<MenuProductDTO>.Ok(menuProduct));
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateMenuProduct([FromBody] MenuProductDTO dto)
        {
            OperationStatus result = await _menuProductManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Menü ürünü oluþturulamadý. Durum: {result}"));

            return Ok(ApiResponse<MenuProductDTO>.Ok(dto, "Menü ürünü baþarýyla oluþturuldu."));
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> SoftDeleteMenuProduct(int id)
        {
            OperationStatus result = await _menuProductManager.SoftDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Menü ürünü bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Menü ürünü silindi (soft delete)."));
        }
    }
}
