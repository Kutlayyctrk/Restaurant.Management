using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs
{
    public class AddRoleVm
    {
        [Required(ErrorMessage = "Rol adı zorunludur.")]
        [StringLength(30, ErrorMessage = "Rol adı en fazla 30 karakter olmalıdır.")]
        public string Name { get; set; }
    }
}
