using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class EditPersonnelVm
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string TCKNo { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int SelectedRoleId { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
