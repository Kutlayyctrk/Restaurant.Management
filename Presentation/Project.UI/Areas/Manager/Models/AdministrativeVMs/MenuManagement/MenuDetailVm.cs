using Project.Domain.Entities.Concretes;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuManagement
{
    public class MenuDetailVm
    {

        public int Id { get; set; }
        public string MenuName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public List<MenuProductInMenuDetailVm> MenuProducts { get; set; } = new();
    }
}
