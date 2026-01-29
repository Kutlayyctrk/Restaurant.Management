using Project.Domain.Enums;

namespace Project.UI.Models.SaleOrderVms
{
    public class OrderDeatilVm
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public OrderDetailStatus DetailState { get; set; }
    }
}
