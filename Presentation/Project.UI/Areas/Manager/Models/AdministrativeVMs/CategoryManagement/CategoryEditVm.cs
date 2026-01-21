using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement
{
    public class CategoryEditVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }

       
        public List<SelectListItem>? ParentCategoryList { get; set; }
    }
}
