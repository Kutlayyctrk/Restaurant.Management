using Project.Application.DTOs;
using System;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class ProductDetailVm
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public bool IsSellable { get; set; }
        public bool IsExtra { get; set; }
        public bool CanBeProduced { get; set; }
        public bool IsReadyMade { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string Status { get; set; }
    }
}