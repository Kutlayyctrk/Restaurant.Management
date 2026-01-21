using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement
{
    public class CategoryCreateVm
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<SelectListItem>? ParentCategoryList { get; set; }
    }
}
