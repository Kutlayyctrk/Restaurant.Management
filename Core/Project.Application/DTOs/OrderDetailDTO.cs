using Project.Domain.Enums;
using System;

namespace Project.Application.DTOs
{
    public class OrderDetailDTO : BaseDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public OrderDetailStatus DetailState { get; set; }


        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DataStatus DataStatus { get; set; }
    }
}