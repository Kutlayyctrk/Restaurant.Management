using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class MenuProduct:BaseEntity
    {
        public int MenuId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }


        public virtual Menu Menu { get; set; }
        public virtual Product Product { get; set; }

      

    }
}
