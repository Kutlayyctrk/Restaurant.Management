namespace Project.UI.Areas.Manager.Models.KitchenVMs
{
    public class CreateMenuProductVm
    {
        public int MenuId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
