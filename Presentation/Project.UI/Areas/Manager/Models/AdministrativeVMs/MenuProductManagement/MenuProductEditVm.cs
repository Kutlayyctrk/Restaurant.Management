using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuProductManagement
{
    public class MenuProductEditVm
    {
        public int Id { get; set; }
        [Required]
        public int MenuId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem>? MenuList { get; set; }
        public List<SelectListItem>? ProductList { get; set; }
    }
}
