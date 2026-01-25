namespace Project.Application.DTOs
{
    public class MenuDTO: BaseDto
    {

        public string MenuName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int? ProductCount  { get; set; }


    }
}