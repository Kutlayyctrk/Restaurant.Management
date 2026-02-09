using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableManager _tableManager;

        public TablesController(ITableManager tableManager)
        {
            _tableManager = tableManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<TableDTO> tables = await _tableManager.GetActives();
            return Ok(ApiResponse<List<TableDTO>>.Ok(tables));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TableDTO table = await _tableManager.GetByIdAsync(id);
            if (table == null)
                return NotFound(ApiResponse<TableDTO>.Fail("Masa bulunamadý."));

            return Ok(ApiResponse<TableDTO>.Ok(table));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TableDTO dto)
        {
            OperationStatus result = await _tableManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Masa oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<TableDTO>.Ok(dto, "Masa baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TableDTO dto)
        {
            TableDTO original = await _tableManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Masa bulunamadý."));

            OperationStatus result = await _tableManager.UpdateAsync(original, dto);
            if (result != OperationStatus.Success)
                return BadRequest(ApiResponse<string>.Fail($"Masa güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Masa baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            OperationStatus result = await _tableManager.SoftDeleteByIdAsync(id);
            if (result == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Masa bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Masa silindi (soft delete)."));
        }
    }
}
