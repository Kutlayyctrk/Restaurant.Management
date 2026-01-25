using Project.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Project.Application.DTOs
{
    public class OrderDTO : BaseDto
    {
        public int? TableId { get; set; }
        public int? WaiterId { get; set; }
        public int? SupplierId { get; set; }

        public string? TableName { get; set; }
        public string? WaiterFullName { get; set; }
        public string? SupplierName { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderState { get; set; }
        public OrderType Type { get; set; }

        public List<OrderDetailDto>? OrderDetails { get; set; }
    }
}