using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class EditPersonnelVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "E-posta adresi zorunludur."), EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ad zorunludur.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "TCK No zorunludur.")]
        public string TCKNo { get; set; }
        [Required (ErrorMessage = "Maaş zorunludur.")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "İşe başlama tarihi zorunludur.")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        public DateTime BirthDate { get; set; }
        public int SelectedRoleId { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
