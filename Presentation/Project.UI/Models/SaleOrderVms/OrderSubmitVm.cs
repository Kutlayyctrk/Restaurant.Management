namespace Project.UI.Models.SaleOrderVms
{
    public class OrderSubmitVm
    {
        public int TableId { get; set; }
       
        public List<OrderDeatilVm> Details { get; set; } = new List<OrderDeatilVm>();
    }
}
