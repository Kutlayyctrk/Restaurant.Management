using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class RecipeItem : BaseEntity
    {
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public int RecipeId { get; set; }

        //Relational Properties

        public virtual Unit Unit { get; set; }
        public virtual Product Product { get; set; }
        public virtual Recipe Recipe { get; set; }



    }
}
