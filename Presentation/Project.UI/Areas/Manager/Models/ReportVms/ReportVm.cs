namespace Project.UI.Areas.Manager.Models.ReportVms
{
    public class ReportVm

    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public Dictionary<string, int> RoleDistribution { get; set; } = new();

    }
}
