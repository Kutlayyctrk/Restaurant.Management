using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.UI.Areas.Manager.Models.HRVMs
{
    public class PersonelEditVm
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int ProfileId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "TCKNo 11 rakam olmalıdır.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TCKNo 11 karakter olmalıdır.")]
        public string TCKNo { get; set; }

        public decimal Salary { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime HireDate { get; set; }


        public int SelectedRoleId { get; set; }
        public List<SelectListItem>? Roles { get; set; }
        public List<string> CurrentRoles { get; set; } = new();
        public List<(int RoleId, string RoleName)> RoleList { get; set; } = new(); 
    }
}