using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Application.DTOs;
using Project.Application.Managers;

using Project.UI.Models.SaleOrderVms;
using System.Security.Cryptography.Xml;

namespace Project.UI.Controllers
{
    public class SalesController : Controller
    {
        private readonly ITableManager _tableManager;
        public SalesController(ITableManager tableManager)
        {
            _tableManager = tableManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId= User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(userId==null)
            {
                return RedirectToAction("Login", "LoginAndRegister");
            }

            List<TableDTO> tableDtos = await _tableManager.GetTablesByUserIdAsync(userId);

            List<TableVm> tableVms = tableDtos.Select(dto => new TableVm
            {
                Id = dto.Id,
                TableNumber = dto.TableNumber,
                Status = dto.TableStatus.ToString(),
                WaiterId = dto.WaiterId
            }).ToList();

            return View(tableVms);



         
        }
    }
}
