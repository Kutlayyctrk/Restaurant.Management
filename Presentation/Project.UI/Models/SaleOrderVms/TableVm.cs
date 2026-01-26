namespace Project.UI.Models.SaleOrderVms
{
    public class TableVm
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public string Status { get; set; }
        public int? WaiterId { get; set; }
    }
}
