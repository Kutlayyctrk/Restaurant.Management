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

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            PagedResult<TableDTO> pagedResult = await _tableManager.GetPagedAsync(page, pageSize);
            return Ok(ApiResponse<PagedResult<TableDTO>>.Ok(pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TableDTO table = await _tableManager.GetByIdAsync(id);
            if (table == null)
                return NotFound(ApiResponse<TableDTO>.Fail("Masa bulunamadı."));

            return Ok(ApiResponse<TableDTO>.Ok(table));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TableDTO dto)
        {
            Result result = await _tableManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Masa oluşturulamadı. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<TableDTO>.Ok(dto, "Masa başarıyla oluşturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TableDTO dto)
        {
            TableDTO original = await _tableManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Masa bulunamadı."));

            Result result = await _tableManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Masa güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Masa başarıyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _tableManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Masa bulunamadı."));

            return Ok(ApiResponse<string>.Ok("Başarılı", "Masa silindi (soft delete)."));
        }
    }
}
