using Project.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Project.Application.DTOs
{
    public class OrderDTO : BaseDto
    {
        public int TableId { get; set; }
        public int? WaiterId { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderStatus OrderState { get; set; }


        public IList<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();

        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DataStatus DataStatus { get; set; }
    }
}