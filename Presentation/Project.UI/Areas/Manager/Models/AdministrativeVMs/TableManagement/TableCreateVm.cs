using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.TableManagement
{
    public class TableCreateVm
    {
        public string TableNumber { get; set; }
        public string TableName { get; set; }
        public int? WaiterId { get; set; }
        public List<SelectListItem> WaiterList { get; set; }
    }
}
