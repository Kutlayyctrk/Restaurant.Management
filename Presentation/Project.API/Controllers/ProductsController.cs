using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductDTO> products = await _productManager.GetActives();
            return Ok(ApiResponse<List<ProductDTO>>.Ok(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDTO product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound(ApiResponse<ProductDTO>.Fail("Ürün bulunamadý."));

            return Ok(ApiResponse<ProductDTO>.Ok(product));
        }

        [HttpGet("sellable")]
        public async Task<IActionResult> GetSellableProducts()
        {
            List<ProductDTO> products = await _productManager.GetSellableProductsAsync();
            return Ok(ApiResponse<List<ProductDTO>>.Ok(products));
        }

        [HttpGet("with-category")]
        public async Task<IActionResult> GetWithCategory()
        {
            List<ProductDTO> products = await _productManager.GetWithCategory();
            return Ok(ApiResponse<List<ProductDTO>>.Ok(products));
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            List<ProductDTO> products = await _productManager.GetProductsByCategoryIdAsync(categoryId);
            return Ok(ApiResponse<List<ProductDTO>>.Ok(products));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO dto)
        {
            OperationStatus result = await _productManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Ürün oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<ProductDTO>.Ok(dto, "Ürün baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO dto)
        {
            ProductDTO original = await _productManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Ürün bulunamadý."));

            OperationStatus result = await _productManager.UpdateAsync(original, dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Ürün güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Ürün baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            OperationStatus result = await _productManager.SoftDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Ürün bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Ürün silindi (soft delete)."));
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(int id)
        {
            OperationStatus result = await _productManager.HardDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Ürün bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Ürün kalýcý olarak silindi."));
        }

        [HttpPost("{id}/increase-stock")]
        public async Task<IActionResult> IncreaseStock(int id, [FromQuery] decimal quantity)
        {
            await _productManager.IncreaseStockAsync(id, quantity);
            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Stok artýrýldý."));
        }
    }
}
