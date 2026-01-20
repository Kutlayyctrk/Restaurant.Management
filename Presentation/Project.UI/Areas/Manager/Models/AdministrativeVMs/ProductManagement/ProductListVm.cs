using Project.Application.DTOs;
using System.Collections.Generic;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class ProductListVm
    {
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SearchTerm { get; set; }
        public bool? IsSellable { get; set; }
        public bool? IsExtra { get; set; }
        public bool? CanBeProduced { get; set; }
        public bool? IsReadyMade { get; set; }
    }
}