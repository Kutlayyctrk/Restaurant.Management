using Project.Domain.Enums;

namespace Project.UI.Areas.Manager.Models.BarAndKitchenVMs
{
    public class OrderVm
    {
        public int OrderId { get; set; }
        public string TableName { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderState { get; set; }
        public List<OrderDetailPureVm> OrderDetails { get; set; }
    }
}