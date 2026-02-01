using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.HRVMs
{
    public class CreateRoleVm
    {
        [Required(ErrorMessage = "Rol adı zorunludur.")]
        public string Name { get; set; }


    }
}
