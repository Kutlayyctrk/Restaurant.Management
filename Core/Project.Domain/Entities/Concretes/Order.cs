using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Domain.Entities.Abstract;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class Order : BaseEntity
    {
        public int TableId { get; set; }
        public int? WaiterId { get; set; } //Masaya bakan garson

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderStatus OrderState { get; set; }



        //Relational Properties

        public virtual Table Table { get; set; }
        public virtual AppUser Waiter { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
