namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuManagement
{
    public class MenuVm
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
