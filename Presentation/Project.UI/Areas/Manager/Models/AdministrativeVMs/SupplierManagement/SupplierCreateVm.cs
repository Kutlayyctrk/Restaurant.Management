using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.SupplierManagement
{
    public class SupplierCreateVm
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }
    }

}
