using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuProductManagement
{
    public class MenuProductCreateVm
    {

        [Required(ErrorMessage = "Menu seçimi zorunlu alandır.")]
        public int MenuId { get; set; }
        [Required(ErrorMessage = "Ürün seçimi zorunlu alandır.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Birim fiyat zorunlu alandır.")]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;

        public List<SelectListItem>? MenuList { get; set; }
        public List<SelectListItem>? ProductList { get; set; }
    }
}
