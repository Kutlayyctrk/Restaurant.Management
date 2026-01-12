using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class StockTransActionDTO:BaseDto
    {
        public int ProductId { get; set; }


        public int? SupplierId { get; set; }


        public int? AppUserId { get; set; }

        public decimal Quantity { get; set; }

        public DateTime? InvoiceDate { get; set; }


        public string TransActionType { get; set; } 

        public string? Description { get; set; }
    }
}
