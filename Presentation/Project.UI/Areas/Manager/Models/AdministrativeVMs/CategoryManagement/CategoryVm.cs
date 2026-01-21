namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public int SubCategoryCount { get; set; }
        public int ProductCount { get; set; }
    }
}

