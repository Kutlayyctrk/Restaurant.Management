using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class RecipeDTO:BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

       
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        
        public string? ProductName { get; set; }  
        public string? CategoryName { get; set; }

        
        public List<RecipeItemDTO>? RecipeItems { get; set; }

    



    }
}
