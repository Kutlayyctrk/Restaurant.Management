using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
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
            OperationStatus result = await _supplierManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Tedarikçi oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<SupplierDTO>.Ok(dto, "Tedarikçi baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDTO dto)
        {
            SupplierDTO original = await _supplierManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Tedarikçi bulunamadý."));

            OperationStatus result = await _supplierManager.UpdateAsync(original, dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Tedarikçi güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Tedarikçi baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            OperationStatus result = await _supplierManager.SoftDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Tedarikçi bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Tedarikçi silindi (soft delete)."));
        }
    }
}
