namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.Reports
{
    public class AdminReportsVm
    {
        // Genel Özet
        public int TotalPersonnelCount { get; set; }
        public int TotalProductCount { get; set; }
        public int TotalCategoryCount { get; set; }
        public int TotalSupplierCount { get; set; }
        public int TotalTableCount { get; set; }
        public int TotalMenuCount { get; set; }

        // Sipariþ Raporu
        public int ActiveOrderCount { get; set; }
        public int FilteredSaleOrderCount { get; set; }
        public int FilteredPurchaseOrderCount { get; set; }
        public decimal FilteredSaleTotalRevenue { get; set; }
        public decimal FilteredPurchaseTotalCost { get; set; }

        // Günlük Ciro
        public decimal TodayRevenue { get; set; }

        // Filtre
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
