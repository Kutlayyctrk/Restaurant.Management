using Project.Domain.Entities.Abstract;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class StockTransAction : BaseEntity
    {
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }
        public int? SupplierId { get; set; }
        public int? OrderDetailId { get; set; }
        public TransActionType Type { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }

        //Relational Properties

        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual Product Product { get; set; }
        public virtual Supplier? Supplier { get; set; }
      

    }
}
