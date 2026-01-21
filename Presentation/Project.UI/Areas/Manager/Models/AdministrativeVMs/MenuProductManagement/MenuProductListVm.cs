using Microsoft.AspNetCore.Mvc.Rendering;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuManagement;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuProductManagement
{
    public class MenuProductListVm
    {
        public List<MenuProductVm> MenuProducts { get; set; }
        public int? SelectedMenuId { get; set; }
        public List<SelectListItem> MenuList { get; set; }
        public string SearchTerm { get; set; }
    }
}
