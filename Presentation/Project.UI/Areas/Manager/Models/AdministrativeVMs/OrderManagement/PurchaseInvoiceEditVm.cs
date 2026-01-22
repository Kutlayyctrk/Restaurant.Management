using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Domain.Enums;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement
{
    public class PurchaseInvoiceEditVm
    {
        public int? Id { get; set; }
        public int? SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; } 
        public List<OrderDetailDTO> Details { get; set; } = new();
       
        public List<SelectListItem>? SupplierList { get; set; } = new();
        public List<SelectListItem>? ProductList { get; set; } = new();
    }

}
