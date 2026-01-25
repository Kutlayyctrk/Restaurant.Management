using Project.Domain.Enums;
using System;

namespace Project.Application.DTOs
{
    public class OrderDetailDTO : BaseDto
    {
       

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Satış anındaki birim fiyat
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LineTotal => (Quantity * UnitPrice) - DiscountAmount;
        public OrderDetailStatus DetailState { get; set; }
    }
}