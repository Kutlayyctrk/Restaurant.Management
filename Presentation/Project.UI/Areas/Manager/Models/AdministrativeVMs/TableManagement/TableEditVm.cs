using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.TableManagement
{
    public class TableEditVm
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public string TableName { get; set; }
    }
}
