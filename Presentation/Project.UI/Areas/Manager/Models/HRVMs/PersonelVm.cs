namespace Project.UI.Areas.Manager.Models.HRVMs
{
    public class PersonelVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime InsertedDate { get; set; }
        public string Roles { get; set; }
    }
}