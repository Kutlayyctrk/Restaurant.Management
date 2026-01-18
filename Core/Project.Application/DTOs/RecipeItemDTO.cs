using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class RecipeItemDTO:BaseDto
    {
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }

        public int UnitId { get; set; }


        public string? ProductName { get; set; }
        public string? UnitName { get; set; }
    }
}
