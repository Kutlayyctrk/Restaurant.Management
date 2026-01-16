using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.UI.Areas.Manager.Models.AppUserVMs
{
    public class PersonelEditVm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

     
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "Maaş")]
        public decimal? Salary { get; set; }

        [Display(Name = "İşe Başlama Tarihi")]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

   
        [Display(Name = "Roller")]
        public IList<int> RoleIds { get; set; } = new List<int>();

  
        public IEnumerable<SelectListItem> RolesSelectList { get; set; } = new List<SelectListItem>();
    }
}