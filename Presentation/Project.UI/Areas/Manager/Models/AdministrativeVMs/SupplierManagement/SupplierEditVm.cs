using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.SupplierManagement
{
    public class SupplierEditVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tedarikçi adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tedarikçi adı 3 ile 100 karakter arasında olmalıdır.")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "İletişim adı zorunludur.")]
        [StringLength(100, ErrorMessage = "İletişim adı en fazla 100 karakter olabilir.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        public string Address { get; set; }
    }
}
