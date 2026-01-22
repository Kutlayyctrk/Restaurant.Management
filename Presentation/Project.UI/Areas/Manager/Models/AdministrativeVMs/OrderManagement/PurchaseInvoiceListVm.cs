using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement
{
    public class PurchaseInvoiceListVm
    {
        public List<OrderDTO> Invoices { get; set; } = new();
        public int? SelectedSupplierId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<SelectListItem> SupplierList { get; set; } = new();
    }
}
