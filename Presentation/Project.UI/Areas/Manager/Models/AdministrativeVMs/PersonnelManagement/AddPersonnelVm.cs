using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class AddPersonnelVm
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCKNo { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Rol seçimi zorunludur.")]
        public int SelectedRoleId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Roles { get; set; }
    }

}