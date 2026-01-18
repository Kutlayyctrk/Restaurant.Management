using Project.Domain.Entities.Abstract;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public OrderDetailStatus DetailState { get; set; }


        //Relational Properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
