using System;

namespace Project.Application.DTOs
{
    public class ProductDTO : BaseDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool IsSellable { get; set; }   
        public bool IsExtra { get; set; }     
        public bool CanBeProduced { get; set; }
        public bool IsReadyMade { get; set; }

        public int UnitId { get; set; }
        public int CategoryId { get; set; }


        public string? UnitName { get; set; }      
        public string? CategoryName { get; set; }  
        public bool HasRecipe => RecipeId != null;
        public int? RecipeId { get; set; }

    }
}