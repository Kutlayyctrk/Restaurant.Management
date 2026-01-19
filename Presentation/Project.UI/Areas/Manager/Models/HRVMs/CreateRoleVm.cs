using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.HRVMs
{
    public class CreateRoleVm
    {
        [Required]
        public string Name { get; set; }


    }
}
