using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement
{
    public class CategoryEditVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori adı zorunlu alandır.")]
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }

       
        public List<SelectListItem>? ParentCategoryList { get; set; }
    }
}
