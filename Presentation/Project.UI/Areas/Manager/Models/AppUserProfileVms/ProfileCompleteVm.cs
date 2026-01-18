using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AppUserProfileVms
{
    public class ProfileCompleteVm
    {
        public int AppUserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string TCKNo { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime HireDate { get; set; }

        public int SelectedRoleId { get; set; }
        public List<SelectListItem> Roles { get; set; } = new();

    }
}
