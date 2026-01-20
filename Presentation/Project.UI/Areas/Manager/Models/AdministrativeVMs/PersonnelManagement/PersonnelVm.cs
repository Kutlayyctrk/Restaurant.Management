namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class PersonnelVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
        public DateTime HireDate { get; set; }
    }
}
