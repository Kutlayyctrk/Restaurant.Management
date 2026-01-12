using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class OrderDTO:BaseDto
    {
        public int TableId { get; set; }
        public int? WaiterId { get; set; } 

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsClosed { get; set; } 
       
        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DataStatus Status { get; set; }
    }
}
