using Project.Domain.Enums;

namespace Project.UI.Areas.Manager.Models.KitchenVMs
{
    public class RecipeListVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public DateTime InsertedDate { get; set; }
        public string Status { get; set; }


    }
}