using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.RoleVMs
{
    public class CreateRoleVm
    {
        [Required]
        public string Name { get; set; }


    }
}
