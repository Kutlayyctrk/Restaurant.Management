using Microsoft.AspNetCore.Mvc;
using Project.API.Models;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Application.Managers;
using Project.Domain.Enums;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<OrderDTO> orders = await _orderManager.GetActives();
            return Ok(ApiResponse<List<OrderDTO>>.Ok(orders));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            PagedResult<OrderDTO> pagedResult = await _orderManager.GetPagedAsync(page, pageSize);
            return Ok(ApiResponse<PagedResult<OrderDTO>>.Ok(pagedResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OrderDTO order = await _orderManager.GetByIdAsync(id);
            if (order == null)
                return NotFound(ApiResponse<OrderDTO>.Fail("Sipariþ bulunamadý."));

            return Ok(ApiResponse<OrderDTO>.Ok(order));
        }

        [HttpGet("active-sales")]
        public async Task<IActionResult> GetActiveSaleOrders()
        {
            List<OrderDTO> orders = await _orderManager.GetActiveSaleOrdersForKitchenAndBarAsync();
            return Ok(ApiResponse<List<OrderDTO>>.Ok(orders));
        }

        [HttpGet("active-waiter-sales")]
        public async Task<IActionResult> GetActiveSaleOrdersForWaiter()
        {
            List<OrderDTO> orders = await _orderManager.GetACtiveSaleOrderForWaiterAsync();
            return Ok(ApiResponse<List<OrderDTO>>.Ok(orders));
        }

        [HttpGet("by-table/{tableId}")]
        public async Task<IActionResult> GetActiveOrderByTable(int tableId)
        {
            OrderDTO? order = await _orderManager.GetActiveOrderForTableAsync(tableId);
            if (order == null)
                return NotFound(ApiResponse<OrderDTO>.Fail("Bu masaya ait aktif sipariþ bulunamadý."));

            return Ok(ApiResponse<OrderDTO>.Ok(order));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDTO dto)
        {
            Result result = await _orderManager.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<string>.Fail($"Sipariþ oluþturulamadý. Durum: {result}"));

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, ApiResponse<OrderDTO>.Ok(dto, "Sipariþ baþarýyla oluþturuldu."));
        }

        [HttpPut("{id}/state")]
        public async Task<IActionResult> ChangeOrderState(int id, [FromQuery] OrderDetailStatus newState)
        {
            await _orderManager.ChangeOrderStateAsync(id, newState);
            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Sipariþ durumu güncellendi."));
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseOrder(int id)
        {
            await _orderManager.CloseOrderState(id);
            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Sipariþ kapatýldý."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Result result = await _orderManager.SoftDeleteByIdAsync(id);
            if (result.Status == OperationStatus.NotFound)
                return NotFound(ApiResponse<string>.Fail("Sipariþ bulunamadý."));

            return Ok(ApiResponse<string>.Ok("Baþarýlý", "Sipariþ silindi (soft delete)."));
        }
    }
}
