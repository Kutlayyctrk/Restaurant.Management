using Project.Application.DTOs;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement
{
    public class PurchaseInvoiceDetailVm
    {
        public OrderDTO Invoice { get; set; }
        public string SupplierName { get; set; }
        public List<OrderDetailDTO> Details { get; set; } = new();
    }
}
