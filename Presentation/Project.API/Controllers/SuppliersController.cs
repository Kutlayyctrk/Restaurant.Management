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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierManager _supplierManager;

        public SuppliersController(ISupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<SupplierDTO> suppliers = await _supplierManager.GetActives();
            return Ok(ApiResponse<List<SupplierDTO>>.Ok(suppliers));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            PagedResult<SupplierDTO> pagedResult = await _supplierManager.GetPagedAsync(page, pageSize);
            return Ok(ApiResponse<PagedResult<SupplierDTO>>.Ok(pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            SupplierDTO supplier = await _supplierManager.GetByIdAsync(id);
            if (supplier == null)
                return NotFound(ApiResponse<SupplierDTO>.Fail("Tedarikçi bulunamadý."));

            return Ok(ApiResponse<SupplierDTO>.Ok(supplier));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierDTO dto)
        {
            Result result = await _supplierManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Tedarikçi oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<SupplierDTO>.Ok(dto, "Tedarikçi baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDTO dto)
        {
            SupplierDTO original = await _supplierManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Tedarikçi bulunamadý."));

            Result result = await _supplierManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Tedarikçi güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Tedarikçi baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _supplierManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Tedarikçi bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Tedarikçi silindi (soft delete)."));
        }
    }
}
