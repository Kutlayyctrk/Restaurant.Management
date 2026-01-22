using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement
{
    public class SaleInvoiceListVm
    {
        public List<OrderDTO> Invoices { get; set; } = new();
        public int? SelectedTableId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<SelectListItem>? TableList { get; set; } = new();
    }
}
