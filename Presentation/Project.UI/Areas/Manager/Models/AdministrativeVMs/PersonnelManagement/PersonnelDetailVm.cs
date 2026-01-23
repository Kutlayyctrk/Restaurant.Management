
namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class PersonnelDetailVm
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string TCKNo { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> Roles { get; set; }
        public decimal EstimatedSeverancePay { get; set; }
        public decimal EstimatedNoticePay { get; set; }
    }

}