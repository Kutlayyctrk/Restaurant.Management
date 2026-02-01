using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.UnitManagement
{
    public class UnitCreateVm
    {
        [Required(ErrorMessage = "Birim adı zorunludur.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Birim adı 2 ile 30 karakter arasında olmalıdır.")]
        public string UnitName { get; set; }

        [StringLength(10, ErrorMessage = "Birim kısaltması en fazla 10 karakter olabilir.")]
        public string UnitAbbreviation { get; set; }
    }
}
