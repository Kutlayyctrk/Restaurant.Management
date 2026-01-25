using Project.Domain.Entities.Abstract;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class Table : BaseEntity
    {
        public string TableNumber { get; set; }
        public TableStatus TableStatus { get; set; } 

        public int? WaiterId { get; set; } 

        public virtual AppUser Waiter { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
