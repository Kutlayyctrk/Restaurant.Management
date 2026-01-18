using Project.Domain.Enums;

public class KitchenOrderDetailVm
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public OrderDetailStatus DetailState { get; set; }
}