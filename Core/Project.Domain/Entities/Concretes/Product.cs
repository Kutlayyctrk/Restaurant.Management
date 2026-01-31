using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Concretes
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public bool IsSellable { get; set; }
        public bool IsExtra { get; set; }
        public bool CanBeProduced { get; set; }
        public bool IsReadyMade { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
        public int? RecipeId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Unit Unit { get; set; }

        public virtual ICollection<MenuProduct> MenuProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
        public virtual ICollection<StockTransAction> StockTransActions { get; set; }



    }
}
