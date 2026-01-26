using Project.Domain.Enums;
using System.Globalization;

namespace Project.Application.DTOs
{
    public class MenuProductDTO : BaseDto
    {
       
        public int MenuId { get; set; }
        public string? MenuName { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }

        public  int? CategoryId { get; set; }
        public string?   CategoryName { get; set; }






    }

}