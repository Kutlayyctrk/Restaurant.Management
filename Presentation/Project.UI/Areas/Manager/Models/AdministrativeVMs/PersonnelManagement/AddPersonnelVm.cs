using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class AddPersonnelVm
    {
        [Required (ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; }
        [Required (ErrorMessage = "E-posta adresi zorunludur.")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "TCK No zorunludur.")]
        public string TCKNo { get; set; }

        [Required(ErrorMessage = "Maaş zorunludur.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "İşe başlama tarihi zorunludur.")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Rol seçimi zorunludur.")]
        public int SelectedRoleId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Roles { get; set; }
    }

}