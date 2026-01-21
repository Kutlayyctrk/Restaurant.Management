using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuManagement
{
    public class MenuEditVm
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string MenuName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
