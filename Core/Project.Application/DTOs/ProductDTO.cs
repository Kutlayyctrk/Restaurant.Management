using System;

namespace Project.Application.DTOs
{
    public class ProductDTO : BaseDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool IsSellable { get; set; }   // Satılabilir mi? (Örn: Pişmiş yemek)
        public bool IsExtra { get; set; }      // Ekstra malzeme mi? (Örn: Soslar)
        public bool CanBeProduced { get; set; } // Reçetesi var mı?
        public bool IsReadyMade { get; set; }

        public int UnitId { get; set; }
        public int CategoryId { get; set; }

        public string? UnitName { get; set; }      // Örn: "Adet", "Kilogram"
        public string? CategoryName { get; set; }  // Örn: "Ana Yemekler"
        public bool HasRecipe => RecipeId != null;
        public int? RecipeId { get; set; }

    }
}