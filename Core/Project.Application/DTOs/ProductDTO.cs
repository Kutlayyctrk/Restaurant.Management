using System;

namespace Project.Application.DTOs
{
    public class ProductDTO : BaseDto
    {
        public string ProductName { get; set; }

      
        public bool IsSellable { get; set; } = true;

       
        public bool IsExtra { get; set; } = false;

      
        public bool CanBeProduced { get; set; } = false;

       
        public bool IsReadyMade { get; set; } = false;

        public decimal UnitPrice { get; set; }
        public int UnitId { get; set; }
        public int CategoryId { get; set; }

       
        public string? UnitName { get; set; }
        public string? CategoryName { get; set; }
    }
}