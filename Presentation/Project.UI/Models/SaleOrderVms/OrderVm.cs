using Project.Domain.Entities.Concretes;

namespace Project.UI.Models.SaleOrderVms
{
    public class OrderVm
    {
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public int? ActiveOrderId { get; set; }
        public List<CategoryVm> Categories { get; set; } = new();
        public List<OrderDeatilVm> ExistingDetails { get; set; } = new();
    }
}
