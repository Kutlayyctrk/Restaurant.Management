using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuManagement
{
    public class MenuEditVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunlu alandır.")]
        [MaxLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi zorunlu alandır.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunlu alandır.")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
