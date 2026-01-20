using Project.Application.Enums;
using Project.UI.Areas.Manager.Models.HRVMs;

namespace Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement
{
    public class PersonnelListVm
    {
        public List<PersonnelVm> Personnel { get; set; }
        public PersonnelFilterType Filter { get; set; }
    }
}
