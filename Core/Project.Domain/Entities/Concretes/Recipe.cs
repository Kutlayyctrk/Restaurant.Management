using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class Recipe : BaseEntity
    {

        public string Description { get; set; }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        //Relational Properties

        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
