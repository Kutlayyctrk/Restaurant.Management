using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;

namespace Project.UI.Areas.Manager.Models.KitchenVMs
{
    public class RecipeEditPageVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public List<RecipeItemDTO> Items { get; set; } = new();


    }



}

