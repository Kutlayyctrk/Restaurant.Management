using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class ProductEditVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Ürün adı 3 ile 150 karakter arasında olmalıdır.")]
        public string ProductName { get; set; }

        [Required (ErrorMessage = "Fiyat zorunludur.")]
        public decimal UnitPrice { get; set; }

        [Required (ErrorMessage = "Birim seçimi zorunludur.")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public int CategoryId { get; set; }

        public bool IsSellable { get; set; }
        public bool IsExtra { get; set; }
        public bool CanBeProduced { get; set; }
        public bool IsReadyMade { get; set; }

        public string Status { get; set; }
    }
}