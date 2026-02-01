using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.HRVMs
{
    public class ProfileCompleteVm
    {
        public int AppUserId { get; set; }
        [Required(ErrorMessage ="Email zorunludur.")]
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage ="Ad zorunludur.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Soyad zorunludur.")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "TCKNo zorunludur.")]
        public string TCKNo { get; set; }
        [Required(ErrorMessage = "Maaş zorunludur.")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "İşe başlama tarihi zorunludur.")]
        public DateTime HireDate { get; set; }

        public int SelectedRoleId { get; set; }
        public List<SelectListItem> Roles { get; set; } = new();

    }
}
