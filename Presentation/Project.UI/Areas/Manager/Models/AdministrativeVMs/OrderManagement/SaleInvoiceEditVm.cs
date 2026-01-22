using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement
{
    public class SaleInvoiceEditVm
    {
        public int? Id { get; set; }
        public int TableId { get; set; }
        public int WaiterId { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string WaiterName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDTO> Details { get; set; } = new();
        public List<SelectListItem>? ProductList { get; set; } = new();
    }
}
