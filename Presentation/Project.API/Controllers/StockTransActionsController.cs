using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Managers;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransActionsController : ControllerBase
    {
        private readonly IStockTransActionManager _stockTransActionManager;

        public StockTransActionsController(IStockTransActionManager stockTransActionManager)
        {
            _stockTransActionManager = stockTransActionManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<StockTransActionDTO> actions = await _stockTransActionManager.GetActives();
            return Ok(ApiResponse<List<StockTransActionDTO>>.Ok(actions));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            StockTransActionDTO action = await _stockTransActionManager.GetByIdAsync(id);
            if (action == null)
                return NotFound(ApiResponse<StockTransActionDTO>.Fail("Stok hareketi bulunamadı."));

            return Ok(ApiResponse<StockTransActionDTO>.Ok(action));
        }
    }
}
