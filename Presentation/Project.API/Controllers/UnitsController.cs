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
    public class UnitsController : ControllerBase
    {
        private readonly IUnitManager _unitManager;

        public UnitsController(IUnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UnitDTO> units = await _unitManager.GetActives();
            return Ok(ApiResponse<List<UnitDTO>>.Ok(units));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UnitDTO unit = await _unitManager.GetByIdAsync(id);
            if (unit == null)
                return NotFound(ApiResponse<UnitDTO>.Fail("Birim bulunamadý."));

            return Ok(ApiResponse<UnitDTO>.Ok(unit));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnitDTO dto)
        {
            Result result = await _unitManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Birim oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<UnitDTO>.Ok(dto, "Birim baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UnitDTO dto)
        {
            UnitDTO original = await _unitManager.GetByIdAsync(id);
            if (original == null)
                return NotFound(ApiResponse<string>.Fail("Birim bulunamadý."));

            Result result = await _unitManager.UpdateAsync(original, dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Birim güncellenemedi. Durum: {result}"));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Birim baþarýyla güncellendi."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _unitManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Birim bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Birim silindi (soft delete)."));
        }
    }
}
