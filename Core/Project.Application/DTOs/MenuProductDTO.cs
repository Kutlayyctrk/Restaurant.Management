using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class MenuProductDTO : BaseDto
    {
       
        public int MenuId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }

      
        public string MenuName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }

  

    }

}