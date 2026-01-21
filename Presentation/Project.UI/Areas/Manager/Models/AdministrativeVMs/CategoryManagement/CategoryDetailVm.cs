namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement
{
    public class CategoryDetailVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ParentCategoryName { get; set; }
        public List<CategoryVm> SubCategories { get; set; }
        public List<CategoryVm> ParentCategories { get; set; }
    }
}
