using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class Unit : BaseEntity
    {
        public string UnitName { get; set; }
        public string UnitAbbreviation { get; set; }

        //Relational Properties

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }

    }
}
