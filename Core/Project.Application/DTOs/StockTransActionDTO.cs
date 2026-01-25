using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class StockTransActionDTO:BaseDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public int? OrderDetailId { get; set; }
        public TransActionType Type { get; set; }
        public string? Description { get; set; }
        
    }
}
