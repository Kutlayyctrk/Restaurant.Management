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
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IMemoryCache _cache;

        public ProductsController(IProductManager productManager, IMemoryCache cache)
        {
            _productManager = productManager;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductDTO> products = await _productManager.GetActives();
            return Ok(ApiResponse<List<ProductDTO>>.Ok(products));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            string cacheKey = $"products_page_{page}_size_{pageSize}";

            if (!_cache.TryGetValue(cacheKey, out PagedResult<ProductDTO> pagedResult))
            {
                pagedResult = await _productManager.GetPagedAsync(page, pageSize);
                _cache.Set(cacheKey, pagedResult, TimeSpan.FromMinutes(5));
            }

            return Ok(ApiResponse<PagedResult<ProductDTO>>.Ok(pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDTO product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound(ApiResponse<ProductDTO>.Fail("Ürün bulunamadı."));

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
            Result result = await _productManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Ürün oluşturulamadı. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<ProductDTO>.Ok(dto, "Ürün başarıyla oluşturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO dto)
        {
            ProductDTO original = await _productManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Ürün bulunamadı."));

            Result result = await _productManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Ürün güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Ürün başarıyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _productManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Ürün bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Ürün silindi (soft delete)."));
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(int id)
        {
            Result result = await _productManager.HardDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Ürün bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Ürün kalıcı olarak silindi."));
        }

        [HttpPost("{id}/increase-stock")]
        public async Task<IActionResult> IncreaseStock(int id, [FromQuery] decimal quantity)
        {
            await _productManager.IncreaseStockAsync(id, quantity);
            return Ok(ApiResponse<string>.Ok("Başarılı", "Stok artırıldı."));
        }
    }
}
