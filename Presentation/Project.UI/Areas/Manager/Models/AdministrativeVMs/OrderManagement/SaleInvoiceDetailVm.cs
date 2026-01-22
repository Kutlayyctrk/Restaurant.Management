using Project.Application.DTOs;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement
{
    public class SaleInvoiceDetailVm
    {
        public OrderDTO Invoice { get; set; }
        public List<OrderDetailDTO> Details { get; set; } = new();
        public string? TableName { get; set; }
    }
}
