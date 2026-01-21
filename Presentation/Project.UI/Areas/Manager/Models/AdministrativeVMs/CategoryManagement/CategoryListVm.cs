namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement
{
    public class CategoryListVm
    {
        public List<CategoryVm> Categories { get; set; }
        public int? SelectedParentCategoryId { get; set; }
        public List<CategoryVm> ParentCategories { get; set; }
    }
}
