using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.UnitManagement
{
    public class UnitCreateVm
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string UnitName { get; set; }

        [StringLength(10)]
        public string UnitAbbreviation { get; set; }
    }
}
