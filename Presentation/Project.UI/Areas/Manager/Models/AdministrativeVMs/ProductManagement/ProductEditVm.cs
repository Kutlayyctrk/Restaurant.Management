using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class ProductEditVm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string ProductName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int UnitId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsSellable { get; set; }
        public bool IsExtra { get; set; }
        public bool CanBeProduced { get; set; }
        public bool IsReadyMade { get; set; }
    }
}