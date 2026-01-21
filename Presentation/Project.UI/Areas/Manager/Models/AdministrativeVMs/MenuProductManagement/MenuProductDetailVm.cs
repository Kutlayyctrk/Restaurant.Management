namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuProductManagement
{
    public class MenuProductDetailVm
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
