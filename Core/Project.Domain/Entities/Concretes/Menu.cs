using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class Menu:BaseEntity
    {
        public string MenuName { get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        
        public virtual ICollection<MenuProduct> MenuProducts { get; set; }

    }
}
